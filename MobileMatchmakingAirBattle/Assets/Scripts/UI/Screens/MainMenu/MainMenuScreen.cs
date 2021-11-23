using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Network.Google;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private PlayerPanel _playerPanel;
        [SerializeField] private Button _leaderBoardButton;
        public override ScreenType Type => ScreenType.MainMenu;

        private void OnEnable()
        {
            _playerPanel.Config();
            _leaderBoardButton.gameObject.SetActive(GooglePlayManager.IsLoad);
        }

        public void SwitchToSearchScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Search).ShowScreen();

        public void SwitchToLeaderboarScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Leaderboard).ShowScreen();

        public void SwitchToOptionsScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Options).ShowScreen();

        public void SwitchToShopScreen() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        //public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.Search).ShowScreen();
    }
}