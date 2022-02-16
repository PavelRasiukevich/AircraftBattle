using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utils.Extensions;

namespace GameObjectComponents
{
    public class PropertiesObserver : MonoBehaviourPunCallbacks
    {
        private AirCraft _airCraft;
        private BodySettings _bodySettings;

        #region UNITY

        private void Awake()
        {
            _airCraft = GetComponent<AirCraft>();
            _bodySettings = GetComponent<BodySettings>();
        }

        #endregion

        #region PUBLIC

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (_airCraft.PhotonView.Owner.ActorNumber == targetPlayer.ActorNumber)
                ChangeBodySettings(changedProps);
        }

        #endregion

        #region PRIVATE

        private void ChangeBodySettings(Hashtable changedProps)
        {
            if (changedProps.TryGetValue(Const.Properties.MaterialColor, out var o))
            {
                _bodySettings.Config(((Vector3) o).ToColor());
            }
        }

        #endregion
    }
}