using Assets.Scripts.Core;
using Assets.Scripts.Network.Launcher;
using Network.External;
using UnityEngine;
using Utils.Enums;

namespace UI.Screens.MainMenu
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private PlayerPanel _playerPanel;
        public override ScreenType Type => ScreenType.MainMenu;

        #region UNITY

        private void OnEnable()
        {
            _playerPanel.Config();
        }

        #endregion

        #region OnClick

        public void SwitchToLeaderboardScreen()
        {
            if (!ExternalServices.Inst.PlayFab.Authenticate.IsReady)
                PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config("Need PlayFab!").Show();
            else
                ScreenHolder.SetCurrentScreen(ScreenType.Leaderboard).ShowScreen();
        }

        public void SwitchToOptionsScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Options).ShowScreen();

        public void SwitchToShopScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        public void MatchingOnClick()
        {
            Launcher.Inst.StartMatching();
            ScreenHolder.SetCurrentScreen(ScreenType.Search).ShowScreen();
        }

        public void AchievementsOnClick()
        {
            if (!ExternalServices.Inst.GooglePlay.Authenticate.IsReady)
                PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config("Need GooglePlay!").Show();
            else
                ExternalServices.Inst.GooglePlay.Achievements.Show();
        }

        #endregion
    }
}