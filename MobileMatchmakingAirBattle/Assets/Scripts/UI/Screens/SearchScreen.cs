using Assets.Scripts.Core;
using Managers.Network.Launcher;
using Utils.Enums;

namespace Assets.Scripts.UI.Screens
{
    public class SearchScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Search;

        #region OnClick

        public void SwitchToMainMenu() => Launcher.Inst.StopMatching();

        #endregion
    }
}