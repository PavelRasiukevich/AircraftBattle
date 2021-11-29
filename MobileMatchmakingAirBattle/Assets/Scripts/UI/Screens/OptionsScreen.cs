using Assets.Scripts.Core;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class OptionsScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Options;

        public void SwitchToMainMenu() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
    }
}