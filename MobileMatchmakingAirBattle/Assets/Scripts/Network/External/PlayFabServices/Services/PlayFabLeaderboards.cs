using Core;
using PlayFab;
using PlayFab.ClientModels;

namespace Network.External.PlayFabServices.Services
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
                DisplayLeaderboard,
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        #endregion

        #region PRIVATE

        private void DisplayLeaderboard(GetLeaderboardResult result) =>
            ScreenEventHolder.Inst.LeaderboardLoad(result.Leaderboard);

        #endregion
    }
}