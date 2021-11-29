using Assets.Scripts.Core;
using Assets.Scripts.Network.Launcher;
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

        public void SwitchToLeaderboarScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Leaderboard).ShowScreen();

        public void SwitchToOptionsScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Options).ShowScreen();

        public void SwitchToShopScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        public void MatchingOnClick()
        {
            Launcher.Instance.StartMatching();
            ScreenHolder.SetCurrentScreen(ScreenType.Search).ShowScreen();
        }

        #endregion
    }
}