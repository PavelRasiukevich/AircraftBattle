using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using UnityEngine;

namespace UI.Screens.MainMenu
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private PlayerPanel _playerPanel;
        public override ScreenType Type => ScreenType.MainMenu;

        private void OnEnable()
        {
            _playerPanel.Config();
        }

        public void SwitchToSearchScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Search).ShowScreen();

        public void SwitchToLeaderboarScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Leaderboard).ShowScreen();

        public void SwitchToOptionsScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Options).ShowScreen();

        public void SwitchToShopScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();
    }
}