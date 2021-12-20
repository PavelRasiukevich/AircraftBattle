using Core;
using Photon.Pun;

namespace Managers.Network.Launcher
{
    public class Launcher : BaseInstance<Launcher>
    {
        #region UNITY

        protected override void Awake()
        {
            base.Awake();

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
        }

        #endregion

        #region PUBLIC

        public void StartMatching() => PhotonNetwork.ConnectUsingSettings();

        public void StopMatching() => PhotonNetwork.Disconnect();

        #endregion
    }
}