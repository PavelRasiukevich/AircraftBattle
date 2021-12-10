using System;
using Core;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Network.External.GooglePlayServices.Services
{
    /*
     * Авторизация GooglePlay
     */
    public class BaseGooglePlayAuthenticate
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
                        ExternalServices.Inst.PlayFab.Authenticate.LoginWithGoogle(PlayGamesPlatform.Instance
                            .GetServerAuthCode());
                    }
                    else
                        ScreenEventHolder.Inst.ErrorGooglePlay("Authenticate success False!");
                });
            }
            catch (Exception e)
            {
                ScreenEventHolder.Inst.ErrorGooglePlay(e.Message);
            }
#endif
        }
    }
}