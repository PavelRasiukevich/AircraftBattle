using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using Core;
using Interfaces.Subscriber;
using Photon.Pun;
using Photon.Realtime;
using System;
using TO;
using UI.Screens.BattleScreen;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviourPunCallbacks, IDamageable
    {
        [SerializeField] private AircraftDataModel _dataModel;
        public AircraftDataModel Data => _dataModel;

        #region ACTIONS

        public Action DieAction { get; set; }

        #endregion

        #region COMPONENTS

        private InputSystemHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
        public PhotonView PhotonView => _photonView;

        private Rigidbody _rigidBody;
        private AircraftParticles _aircraftParticles;

        #endregion

        #region UNITY

        private void Awake()
        {
            _inputHandler = GetComponent<InputSystemHandler>();
            _moveHandler = GetComponent<MoveHandler>();
            _attackHandler = GetComponent<AttackHandler>();
            _photonView = GetComponent<PhotonView>();
            _rigidBody = GetComponent<Rigidbody>();
            _aircraftParticles = GetComponent<AircraftParticles>();
            _attackHandler.PhotonView = _photonView;

            _attackHandler.Aircraft = this;

            _inputHandler.Attacking += _attackHandler.Attack;
        }

        private void Start() =>
            EventBus<BattleScreen>.InvokeEvent(h =>
                h.RefreshUI(Data));

        private void FixedUpdate()
        {
            if (!_photonView.IsMine) return;

            if (_dataModel.IsControllable)
                _moveHandler.MoveWithJoyStick(_rigidBody, _inputHandler.InputParams, _dataModel.Speed);
            else
                _moveHandler.MoveUncontrollable(_rigidBody, _dataModel.Speed);
        }

        #endregion

        public void TakeDamage(int value, Player owner) =>
            _photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, value, owner);

        [PunRPC]
        private void RPC_TakeDamage(object[] values)
        {
            if (!_photonView.IsMine) return;
            _dataModel.CurrentHp -= (int)values[0];
            EventBus<BattleScreen>.InvokeEvent((x) => x.DamageUI(Data));
            if (_dataModel.CurrentHp <= 0) Die();
        }

        public void Die()
        {
            _aircraftParticles.DestroyEffect();
            DieAction.Invoke();
        }
    }
}