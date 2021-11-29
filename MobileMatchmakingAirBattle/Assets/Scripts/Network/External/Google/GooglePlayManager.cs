using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UI.Screens;
using UnityEngine;

namespace Network.External.Google
{
    /*
     * Управление GooglePlay
     */
    public class GooglePlayManager
    {
        public bool IsLoad { get; private set; }

        private void ActivateGoogle()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .AddOauthScope("profile")
                .RequestServerAuthCode(false)
                .RequestEmail()
                .RequestIdToken()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        public void SignIn()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            ActivateGoogle();
            try
            {
                Social.localUser.Authenticate(success =>
                {
                    IsLoad = success;
                    if (success)
                    {
                        ExternalServicesManager.Instance.PlayFabManager.LoginWithGoogle(PlayGamesPlatform.Instance
                            .GetServerAuthCode());
                    }
                    else
                        ScreenEventHolder.Instance.ErrorGooglePlay("Authenticate success False!");
                });
            }
            catch (Exception e)
            {
                ScreenEventHolder.Instance.ErrorGooglePlay(e.Message);
            }
#endif
        }
    }
}