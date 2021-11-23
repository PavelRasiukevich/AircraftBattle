using System;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Network.Google
{
    public class GooglePlayManager
    {
        private static bool _isLoad = false;

        public static bool IsLoad
        {
            get
            {
#if UNITY_EDITOR
                return false;
#else
                return _isLoad;
#endif
            }
            private set => _isLoad = value;
        }


        private static void ActivateGoogle()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                //.AddOauthScope("profile") TODO: PlayFab CreateRoom Error 
                .RequestServerAuthCode(false)
                .RequestEmail()
                .RequestIdToken()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        public static void SignIn()
        {
            IsLoad = false;
            ActivateGoogle();
            try
            {
                Social.localUser.Authenticate(success =>
                {
                    IsLoad = success;
                    ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
                });
            }
            catch (Exception e)
            {
                Debug.Log("SignIn Exception: " + e.Message);
                ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
            }
        }
    }
}