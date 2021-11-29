using Assets.Scripts.Core;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class LeaderboardScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Leaderboard;

        public void SwitchToMain() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
    }
}