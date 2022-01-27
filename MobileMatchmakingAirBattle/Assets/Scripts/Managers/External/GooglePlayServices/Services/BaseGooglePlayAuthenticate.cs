using System;
using Core;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Interfaces.Subscriber;
using UnityEngine;

namespace Managers.External.GooglePlayServices.Services
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
                        EventBus.InvokeEvent<IStringErrorHandler>(h => h.Error("Authenticate success False!"));
                });
            }
            catch (Exception e)
            {
                EventBus.InvokeEvent<IStringErrorHandler>(h => h.Error(e.Message));
            }
#endif
        }
    }
}