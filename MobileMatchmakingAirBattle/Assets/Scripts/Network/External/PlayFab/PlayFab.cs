using Assets.Scripts.Core;
using PlayFab;
using Utils.Enums;

namespace Network.External.PlayFab
{
    public abstract class PlayFab
    {
        protected void UnexpectedErrorUI(PlayFabError err) =>
            PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config(err.ErrorMessage).Show();
    }
}