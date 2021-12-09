using Network.External.PlayFabServices.Services;

namespace Network.External.PlayFabServices
{
    public class BasePlayFab
    {
        public BasePlayFab()
        {
            Authenticate = new PlayFabAuthenticate();
        }

        public PlayFabAuthenticate Authenticate { get; set; }
        public PlayFabLeaderboards Leaderboards { get; } = new PlayFabLeaderboards();
        public PlayFabStatistics Statistics { get; } = new PlayFabStatistics();
    }
}