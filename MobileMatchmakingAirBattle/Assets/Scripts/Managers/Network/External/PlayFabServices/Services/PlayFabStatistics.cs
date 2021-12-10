using System.Collections.Generic;
using Core;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Network.External.PlayFabServices.Services
{
    public class PlayFabStatistics
    {
        #region PUBLIC

        public void SubmitScore(string statisticName, int newValue)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName = statisticName,
                            Value = newValue
                        }
                    }
                },
                OnStatisticsUpdated,
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult) =>
            Debug.Log("Successfully Statistics Updated ");

        #endregion
    }
}