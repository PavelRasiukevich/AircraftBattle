using Assets.Scripts.Core;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class ShopScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Shop;

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
    }
}