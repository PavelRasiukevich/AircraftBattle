using Core;
using PlayFab;
using PlayFab.ClientModels;

namespace Network.External.PlayFab
{
    public class PlayFabLeaderboards : PlayFab
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
                UnexpectedErrorUI);
        }

        #endregion

        #region PRIVATE

        private void DisplayLeaderboard(GetLeaderboardResult result) =>
            ScreenEventHolder.Instance.LeaderboardLoad(result.Leaderboard);

        #endregion
    }
}