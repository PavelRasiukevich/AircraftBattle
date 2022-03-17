using Assets.Scripts.Core;
using Core;
using Enums;
#if UNITY_ANDROID
using GooglePlayGames;
#endif
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Managers.External.GooglePlayServices.Services
{
    public class BaseGooglePlayAchievements
    {
        #region PUBLIC

        public void Frags(int count)
        {
            switch (count)
            {
                case 1:
                    Report(GPGSIds.achievement_recruit);
                    break;
                case 10:
                    Report(GPGSIds.achievement_warrior);
                    break;
                case 100:
                    Report(GPGSIds.achievement_hunter);
                    break;
                case 500:
                    Report(GPGSIds.achievement_master_hunter);
                    break;
                case 1000:
                    Report(GPGSIds.achievement_destroyer);
                    break;
            }
        }

        public void Wins(int count)
        {
            switch (count)
            {
                case 1:
                    Report(GPGSIds.achievement_stewardess);
                    break;
                case 5:
                    Report(GPGSIds.achievement_novice_pilot);
                    break;
                case 10:
                    Report(GPGSIds.achievement_second_pilot);
                    break;
                case 50:
                    Report(GPGSIds.achievement_fighter_pilot);
                    break;
            }
        }

        public void Kamikaze() => Increment(GPGSIds.achievement_kamikaze);

        public void Show()
        {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ShowAchievementsUI(OnShowAchievementsUI);
#endif
        }

        #endregion

        #region PRIVATE

        private void Report(string id) => Social.ReportProgress(id, 100.0f, success => { });

        private void Increment(string id)
        {
#if UNITY_ANDROID
  PlayGamesPlatform.Instance.IncrementAchievement(id, 1, (bool success) => { });
#endif
        }


        public void OnShowAchievementsUI(UIStatus status) => PopupHolder.CurrentPopup(PopupType.Loading).Hide();

        #endregion
    }
}