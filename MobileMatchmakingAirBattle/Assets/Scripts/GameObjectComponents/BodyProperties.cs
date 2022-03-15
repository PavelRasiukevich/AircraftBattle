using Assets.Scripts.Utils;
using Core;
using Interfaces.EventBus.PlayerProperties;
using Photon.Pun;
using UnityEngine;
using Utils.Extensions;

namespace GameObjectComponents
{
    public class BodyProperties : MonoBehaviour, IBodyParams
    {
        public PhotonView PhotonView { get; set; }
        public BodySettings BodySettings { get; set; }

        #region UNITY

        private void Awake()
        {
            Vector3 clr = PhotonView.Owner.GetPropertyValue(Const.Properties.MaterialColor, Vector3.zero);
            BodySettings.Config(clr.ToColor());
        }

        void OnEnable()
        {
            EventBus.AddListener<IBodyParams>(this);
        }

        void OnDisable()
        {
            EventBus.RemoveListener<IBodyParams>(this);
        }

        #endregion

        public void OnColorChanged(int actorNumber, Vector3 clr)
        {
            if (PhotonView.Owner.ActorNumber == actorNumber)
            {
                BodySettings.Config(clr.ToColor());
            }
        }
    }
}