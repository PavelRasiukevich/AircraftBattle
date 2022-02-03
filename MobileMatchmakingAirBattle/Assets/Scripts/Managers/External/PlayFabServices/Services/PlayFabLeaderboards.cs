using Core;
using PlayFab;
using PlayFab.ClientModels;
using UI.Screens.LeaderBoard;

namespace Managers.External.PlayFabServices.Services
{
    public class PlayFabLeaderboards
    {
        #region PUBLIC

        public void RequestLeaderboard(string statisticName)
        {
            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
                {
                    StatisticName = statisticName,
                    StartPosition = 0,
                    MaxResultsCount = 10
                },
                result => EventBus<LeaderboardScreen>.InvokeEvent(h => h.Refresh(result.Leaderboard)),
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        #endregion
    }
}