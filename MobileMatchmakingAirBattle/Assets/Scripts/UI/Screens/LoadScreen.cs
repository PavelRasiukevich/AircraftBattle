using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Network.Google;

namespace UI.Screens
{
    public class LoadScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Load;

        private void OnEnable() => GooglePlayManager.SignIn();
    }
}