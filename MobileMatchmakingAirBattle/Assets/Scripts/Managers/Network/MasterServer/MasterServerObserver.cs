using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Enums;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Network.MasterServer
{
    public class MasterServerObserver : MonoBehaviourPunCallbacks
    {
        #region PUN CALLBACKS

        public override void OnConnectedToMaster()
        {
            MessagesUtilities.ConnectedToMasterMessage();

            if (!PhotonNetwork.InLobby)
                PhotonNetwork.JoinLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            MessagesUtilities.DisconnectedFromMasterMessage();

            if (Application.isPlaying)
                ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

            //handle possible excepsion
        }

        #endregion
    }
}