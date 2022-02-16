using System;
using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using Core;
using GameObjectComponents;
using Interfaces.EventBus;
using Photon.Pun;
using Photon.Realtime;
using TO;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviourPunCallbacks, IDamageable
    {
        [SerializeField] private AircraftDataModel _dataModel;
        [SerializeField] private DeathZone _deathZone;

        //test value
        [SerializeField] private bool _isContrl;

        public AircraftDataModel Data => _dataModel;

        public PhotonView PhotonView => _photonView;

        private bool _isFail;
        
        #region COMPONENTS

        [SerializeField] private View _view;
        private PlayerInputHandler _playerInputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
        private Rigidbody _rigidBody;
        private AircraftParticles _aircraftParticles;

        #endregion

        #region UNITY

        private void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _moveHandler = GetComponent<MoveHandler>();
            _attackHandler = GetComponent<AttackHandler>();
            _photonView = GetComponent<PhotonView>();
            _rigidBody = GetComponent<Rigidbody>();
            _aircraftParticles = GetComponent<AircraftParticles>();
            _attackHandler.PhotonView = _photonView;

            _attackHandler.Aircraft = this;
            _moveHandler.View = _view.transform;
            _moveHandler.Body = _rigidBody;

            _playerInputHandler.Attacking += _attackHandler.Attack;
        }

        private void Start()
        {
            _isFail = false;
            EventBus.InvokeEvent<IBattleScreenEvents>(h => h.RefreshUI(Data));
        }
            

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
            _dataModel.CurrentHp -= (int) values[0];
            if (_dataModel.CurrentHp <= 0)
            {
                Die();
            }
            else
            {
                EventBus.InvokeEvent<IBattleScreenEvents>(x => x.DamageUI(Data));
            }
        }

        public void Die()
        {
            if (_isFail)return;
            _isFail = true;
            EventBus.InvokeEvent<IDestroy>(x => x.DestroyAircraft());
            PhotonNetwork.Destroy(gameObject);
        }
    }
}