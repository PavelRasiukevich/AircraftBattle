using Core;
using Photon.Pun;

namespace Assets.Scripts.Network.Launcher
{
    public class Launcher : BaseInstance<Launcher>
    {
        #region UNITY

        private void Awake()
        {
            base.Awake();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        #endregion

        #region PUBLIC METHODS

        public void StartMatching() => PhotonNetwork.ConnectUsingSettings();

        public void StopMatching() => PhotonNetwork.Disconnect();

        #endregion
    }
}