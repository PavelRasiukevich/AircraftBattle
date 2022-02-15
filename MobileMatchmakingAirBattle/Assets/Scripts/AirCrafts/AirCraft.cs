using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using Core;
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

        //test value
        [SerializeField] private bool _isContrl;

        public AircraftDataModel Data => _dataModel;

        public PhotonView PhotonView => _photonView;

        #region ACTIONS

        public Action DieAction { get; set; }

        #endregion

        #region COMPONENTS

        [SerializeField] private View _view;
        private InputSystemHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
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
            _moveHandler.View = _view.transform;
            _moveHandler.Body = _rigidBody;

            _inputHandler.Attacking += _attackHandler.Attack;
        }

        private void Start() =>
            EventBus<BattleScreen>.InvokeEvent(h =>
                h.RefreshUI(Data));

        private void FixedUpdate()
        {
            if (!_photonView.IsMine) return;

            if (_dataModel.IsControllable || _isContrl)
                _moveHandler.Pilot(_inputHandler.InputParams, Data.Speed);
            else
                _moveHandler.DragToBattleField(_dataModel.Speed);
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