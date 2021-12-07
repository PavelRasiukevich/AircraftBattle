using Assets.Scripts.Core;
using Assets.Scripts.Network.Launcher;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class SearchScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Search;

        #region OnClick

        public void SwitchToMainMenu() => Launcher.Instance.StopMatching();

        #endregion
    }
}