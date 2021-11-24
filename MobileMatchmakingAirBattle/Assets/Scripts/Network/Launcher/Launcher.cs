using Photon.Pun;

namespace Assets.Scripts.Network.Launcher
{
    public class Launcher : MonoBehaviourPunCallbacks
    {

        #region UNITY

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
        }

        #endregion

        #region PUN CALLBACKS
        #endregion

        #region PUBLIC METHODS
        public void StartMatching() => PhotonNetwork.ConnectUsingSettings();

        public void StopMatching() => PhotonNetwork.Disconnect();
        #endregion
    }
}