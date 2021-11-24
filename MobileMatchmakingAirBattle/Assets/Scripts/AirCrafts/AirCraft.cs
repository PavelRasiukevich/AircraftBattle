using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    [DisallowMultipleComponent]
    public class AirCraft : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
    {
        [SerializeField] private float _speed;

        #region COMPONENTS

        private InputHandler _inputHandler;
        private MoveHandler _moveHandler;
        private AttackHandler _attackHandler;
        private LagCompensator _lagCompensator;
        private PhotonView _photonView;
        private Rigidbody _rigidBody;

        #endregion

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            //TODO
            //get color according
            //to actornumber
        }

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _moveHandler = new MoveHandler();
            _attackHandler = GetComponent<AttackHandler>();
            _lagCompensator = GetComponent<LagCompensator>();
            _photonView = GetComponent<PhotonView>();
            _rigidBody = GetComponent<Rigidbody>();

            _lagCompensator.Init(_rigidBody);
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            

            if (_photonView.IsMine)
                _moveHandler.Move(_rigidBody, _inputHandler);
            else
                _moveHandler.MoveRemote(_rigidBody, _lagCompensator.NetworkPosition);
        }

        private object[] GetInitData()
        {
            object[] data = new object[1];
            data[0] = PhotonNetwork.LocalPlayer.UserId;
            return data;
        }

        #region TEST
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }

        private void TestStuff()
        {
            print(_photonView.ViewID);
            print(_photonView.OwnerActorNr);
            print(_photonView.ControllerActorNr);
            print(_photonView.CreatorActorNr);
            _photonView.TransferOwnership(2001);
        }
        #endregion

    }
}