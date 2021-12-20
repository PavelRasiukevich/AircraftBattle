using Assets.Scripts.Core;
using Managers.External;
using TO;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class OptionsScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Options;

        #region OnClick

        public void SwitchToMainMenu() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

        #endregion

        #region TEST

        public void TestIncFails() => User.Statistic.Fails++;

        public void TestIncFrags() => User.Statistic.Frags++;

        public void TestIncWins() => User.Statistic.Wins++;
        public void TestIncFights() => User.Statistic.Fights++;
        public void TestKamikaze() => ExternalServices.Inst.GooglePlay.Achievements.Kamikaze();

        #endregion
    }
}