using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Network.External.PlayFab
{
    public class PlayFabStatistics : PlayFab
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
                UnexpectedErrorUI);
        }

        private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult) =>
            Debug.Log("Successfully Statistics Updated ");

        #endregion
    }
}