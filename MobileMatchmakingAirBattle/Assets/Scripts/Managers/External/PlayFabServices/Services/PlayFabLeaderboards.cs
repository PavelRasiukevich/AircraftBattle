using Core;
using PlayFab;
using PlayFab.ClientModels;

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
                result => ScreenEventHolder.Inst.RefreshLeaderboardLoad(result.Leaderboard),
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        #endregion
    }
}