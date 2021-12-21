using Managers.External.PlayFabServices.Services;

namespace Managers.External.PlayFabServices
{
    public class BasePlayFab
    {
        public PlayFabAuthenticate Authenticate { get; } = new PlayFabAuthenticate();
        public PlayFabLeaderboards Leaderboards { get; } = new PlayFabLeaderboards();
        public PlayFabStatistics Statistics { get; } = new PlayFabStatistics();
        public PlayFabCurrencies Currencies { get; } = new PlayFabCurrencies();
    }
}