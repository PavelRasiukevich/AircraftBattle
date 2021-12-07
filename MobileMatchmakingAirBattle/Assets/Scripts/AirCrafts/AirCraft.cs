using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback, IDamageable
    {
        [SerializeField] private AircraftDataModel _dataModel;

        #region COMPONENTS

        private InputHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
        private Rigidbody _rigidBody;
        private AircraftCollisionDetector _collisionDetector;

        #endregion

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            //TODO
            //get color according
            //to actornumber
        }

        #region UNITY

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _moveHandler = GetComponent<MoveHandler>();
            _attackHandler = GetComponent<AttackHandler>();
            _photonView = GetComponent<PhotonView>();
            _rigidBody = GetComponent<Rigidbody>();
            _collisionDetector = GetComponent<AircraftCollisionDetector>();

            _attackHandler.PhotonView = _photonView;
            _collisionDetector.AirCraft = this;
        }

        private void FixedUpdate()
        {
            if (!_photonView.IsMine) return;

            _moveHandler.MoveWithJoystickSimple(_rigidBody, _inputHandler.PlayersInput, _dataModel.MoveSpeed);
        }

        #endregion

        public void TakeDamage(int value, Player owner)
        {
            _dataModel.CurrentHp = _dataModel.CurrentHp <= value ? 0 : _dataModel.CurrentHp - value;

            if (_dataModel.CurrentHp != 0) return;
        }
    }
}