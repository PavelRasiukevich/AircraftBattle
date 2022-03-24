using Assets.Scripts.GameObjectComponents;
using Core;
using GameObjectComponents;
using Interfaces.EventBus;
using Photon.Pun;
using TO;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviour
    {
        [SerializeField] private AircraftDataModel _dataModel;
        [SerializeField] private Transform _view;

        public AircraftDataModel Data => _dataModel;

        public PhotonView PhotonView => _photonView;

        #region COMPONENTS

        private InputSystemHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private PhotonView _photonView;
        private Rigidbody _rigidBody;
        private AircraftCollisionDetector _collisionDetector;
        private AircraftParticles _aircraftParticles;
        private InteractionsHandler _interactor;
        private BodyProperties _bodyProperties;
        private BodySettings _bodySettings;

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
            _collisionDetector = GetComponent<AircraftCollisionDetector>();
            _interactor = GetComponent<InteractionsHandler>();
            _bodyProperties = GetComponent<BodyProperties>();
            _bodySettings = GetComponent<BodySettings>();

            _bodyProperties.PhotonView = PhotonView;
            _bodyProperties.BodySettings = _bodySettings;

            _interactor.PhotonView = PhotonView;
            _interactor.DataModel = Data;

            _attackHandler.PhotonView = PhotonView;
            _attackHandler.DataModel = Data;
            _attackHandler.InputHandler = _inputHandler;

            _collisionDetector.Interactor = _interactor;
            _collisionDetector.PhotonView = _photonView;

            _moveHandler.View = _view;
            _moveHandler.Body = _rigidBody;
            _moveHandler.InputHandler = _inputHandler;
            _moveHandler.DataModel = Data;
            _moveHandler.PhotonView = PhotonView;
        }

        private void OnEnable() => _collisionDetector.CrossDome += _moveHandler.Return;

        private void OnDisable() => _collisionDetector.CrossDome -= _moveHandler.Return;

        private void Start()
        {
            EventBus.InvokeEvent<IBattleScreenEvents>(h => h.RefreshUI(Data));
        }

        #endregion
    }
}