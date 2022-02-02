using System;
using Core.Base;
using Managers.Network.Launcher;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class SearchScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Search;

        [SerializeField] private TMP_Text _playerID;

        #region UNITY

        private void Update()
        {
            try
            {
                if (PhotonNetwork.LocalPlayer != null)
                {
                    _playerID.text = $"ID: {PhotonNetwork.LocalPlayer.UserId}";
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        #endregion

        #region OnClick

        public void SwitchToMainMenu() => Launcher.Inst.StopMatching();

        #endregion
    }
}