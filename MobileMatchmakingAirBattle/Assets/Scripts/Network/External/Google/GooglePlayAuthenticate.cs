using System;
using Core;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UI.Screens;
using UnityEngine;

namespace Network.External.Google
{
    /*
     * Управление GooglePlay
     */
    public class GooglePlayAuthenticate : GooglePlay
    {
        public bool IsReady { get; private set; }

        private void ActivateGoogle()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .AddOauthScope("profile")
                .RequestServerAuthCode(false)
                .RequestEmail()
                .RequestIdToken()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
#endif
        }

        public void SignIn()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            ActivateGoogle();
            try
            {
                Social.localUser.Authenticate(success =>
                {
                    IsReady = success;
                    if (success)
                    {
                        ExternalServices.Instance.PlayFabAuthenticate.LoginWithGoogle(PlayGamesPlatform.Instance
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