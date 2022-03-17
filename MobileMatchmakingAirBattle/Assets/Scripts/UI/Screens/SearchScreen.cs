using Core.Base;
using Enums;
using Managers.Network.Launcher;

namespace Assets.Scripts.UI.Screens
{
    public class SearchScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Search;

        #region UNITY
        
        #endregion

        #region OnClick

        public void SwitchToMainMenu() => Launcher.Inst.StopMatching();

        #endregion
    }
}