using Assets.Scripts.Core;
using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using Assets.Scripts.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TO;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback, IDamageable, IObservable
    {
        [SerializeField] private AircraftDataModel _dataModel;
        [SerializeField] private Transform _center;

        private List<IObserver> observers;

        public AircraftDataModel DataModel => _dataModel;

        #region EVENTS
        public Action DieAction { get; set; }
        #endregion

        #region COMPONENTS

        private InputSystemHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
        private Rigidbody _rigidBody;
        private AircraftCollisionDetector _collisionDetector;

        #endregion

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
        }

        #region UNITY

        private void Awake()
        {
            observers = new List<IObserver>
            {
                FindObjectOfType<HealthBar>()
            };

            _inputHandler = GetComponent<InputSystemHandler>();
            _moveHandler = GetComponent<MoveHandler>();
            _attackHandler = GetComponent<AttackHandler>();
            _photonView = GetComponent<PhotonView>();
            _rigidBody = GetComponent<Rigidbody>();
            _collisionDetector = GetComponent<AircraftCollisionDetector>();

            _attackHandler.PhotonView = _photonView;
            _attackHandler.Aircraft = this;
            _collisionDetector.AirCraft = this;
        }

        private void Update()
        {
            _attackHandler.Attack(_inputHandler.InputParams.IsFiring);
        }

        private void FixedUpdate()
        {
            if (!_photonView.IsMine) return;

            if (_dataModel.IsControllable)
                _moveHandler.MoveWithJoyStick(_rigidBody, _inputHandler.InputParams, _dataModel.Speed);
            else
                _moveHandler.MoveUncontrollable(_rigidBody, _dataModel.Speed);
        }

        #endregion

        public void TakeDamage(int value, Player owner)
        {
            _photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, new object[2] { value, owner });
        }

        [PunRPC]
        private void RPC_TakeDamage(object[] values)
        {
            if (!_photonView.IsMine) return;

            _dataModel.CurrentHp = _dataModel.CurrentHp <= (int)values[0] ? 0 : _dataModel.CurrentHp - (int)values[0];

            NotifyObservers();

            if (_dataModel.CurrentHp != 0) return;

            Die();
        }

        private void Die()
        {
            DieAction.Invoke();
        }

        #region Observer

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
                observer.PerformAction(_dataModel.CurrentHp, _dataModel.Hp);
        }

        #endregion
    }
}