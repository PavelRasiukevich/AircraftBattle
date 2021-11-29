using Assets.Scripts.Core;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class SearchScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Search;

        public void SwitchToMainMenu() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
    }
}