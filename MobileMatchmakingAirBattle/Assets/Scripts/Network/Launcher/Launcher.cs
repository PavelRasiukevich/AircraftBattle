using Network.Google;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Network.Launcher
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region UNITY

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        #endregion

        #region PUN CALLBACKS

        #endregion

        #region PUBLIC METHODS

        public void StartMatching()
        {
            if (GooglePlayManager.IsLoad)
                PhotonNetwork.NickName = Social.localUser.userName;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void StopMatching() => PhotonNetwork.Disconnect();

        #endregion
    }
}