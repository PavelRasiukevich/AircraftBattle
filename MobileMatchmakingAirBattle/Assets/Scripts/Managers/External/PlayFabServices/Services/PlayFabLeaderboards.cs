﻿using Core;
using Interfaces.Subscriber;
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
                result => EventBus.InvokeEvent<ILeaderboardLoadHandler>(h => h.Refresh(result.Leaderboard)),
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        #endregion
    }
}