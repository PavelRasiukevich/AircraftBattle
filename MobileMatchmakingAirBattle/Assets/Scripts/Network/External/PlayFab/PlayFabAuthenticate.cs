using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Core;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TO;
using UnityEngine;
using Utils.Enums;

namespace Network.External.PlayFab
{
    /*
     * Управление PlayFab
     */
    public class PlayFabAuthenticate : PlayFab
    {
        public string PlayFabPlayerId { get; private set; } = "";
        public bool IsReady { get; private set; }

        #region public Authenticate

        public void AuthenticateWithCustomId()
        {
            PlayFabSettings.TitleId = UtilsConst.PlayFab.TitleId;
            User.Name = $"PlayFab_PLayer_{Random.Range(0, 100)}";
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = PlayFabSettings.DeviceUniqueIdentifier
            };
            PlayFabClientAPI.LoginWithCustomID(
                request,
                RequestPhotonToken,
                UnexpectedErrorUI
            );
        }

        public void LoginWithPlayFab(string userName, string userPass)
        {
            PlayFabSettings.TitleId = UtilsConst.PlayFab.TitleId;
            User.Name = userName;
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest
            {
                Username = userName,
                Password = userPass
            };
            PlayFabClientAPI.LoginWithPlayFab(
                request,
                RequestPhotonToken,
                ScreenEventHolder.Instance.ErrorLogin
            );
        }

        public void RegisterWithPlayFab(string userName, string userPass, string mail)
        {
            PlayFabSettings.TitleId = UtilsConst.PlayFab.TitleId;
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
            {
                Username = userName,
                Password = userPass,
                Email = mail
            };
            PlayFabClientAPI.RegisterPlayFabUser(
                request,
                res => ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen(),
                ScreenEventHolder.Instance.ErrorRegistration);
        }

        public void LoginWithGoogle(string serverAuthCode)
        {
            PlayFabSettings.TitleId = UtilsConst.PlayFab.TitleId;
            User.Name = Social.localUser.userName;
            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                ServerAuthCode = serverAuthCode,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithGoogleAccount(
                request,
                RequestPhotonToken,
                UnexpectedErrorUI
            );
        }

        #endregion

        #region PRIVATE

        #region Photon Request

        private void RequestPhotonToken(LoginResult result)
        {
            PlayFabPlayerId = result.PlayFabId;
            GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest
            {
                PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
            };
            PlayFabClientAPI.GetPhotonAuthenticationToken(
                request,
                OnAuthenticateWithPhoton,
                UnexpectedErrorUI
            );
        }

        private void OnAuthenticateWithPhoton(GetPhotonAuthenticationTokenResult rezult)
        {
            AuthenticationValues customAuth = new AuthenticationValues
            {
                AuthType = CustomAuthenticationType.Custom
            };
            customAuth.AddAuthParameter("username", PlayFabPlayerId);
            customAuth.AddAuthParameter("token", rezult.PhotonCustomAuthenticationToken);
            PhotonNetwork.AuthValues = customAuth;
            RequestStatistics();
        }

        #endregion

        #region RequestStatistics

        private void RequestStatistics()
        {
            PlayFabClientAPI.GetPlayerStatistics(
                new GetPlayerStatisticsRequest
                {
                    StatisticNames = User.Statistic.Data.Keys.ToList()
                },
                OnStatisticsRequest, UnexpectedErrorUI
            );
        }

        private void OnStatisticsRequest(GetPlayerStatisticsResult result)
        {
            result.Statistics.ForEach(
                rec => User.Statistic.Data[rec.StatisticName] = rec.Value
            );
            RequestPlayerProfile();
        }

        #endregion

        #region RequestPlayerProfile

        private void RequestPlayerProfile()
        {
            PlayFabClientAPI.GetPlayerProfile(
                new GetPlayerProfileRequest(),
                OnPlayerProfileRequest,
                UnexpectedErrorUI
            );
        }

        private void OnPlayerProfileRequest(GetPlayerProfileResult result)
        {
            if (string.IsNullOrEmpty(result.PlayerProfile.DisplayName) ||
                result.PlayerProfile.DisplayName != User.Name)
                UpdateUserDisplayName(User.Name);
            else
                PlayFabAuthenticateDone();
        }

        #endregion

        #region UpdateUserDisplayName

        private void UpdateUserDisplayName(string displayName)
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest()
                {
                    DisplayName = displayName
                },
                OnUserDisplayNameUpdated,
                UnexpectedErrorUI);
        }

        private void OnUserDisplayNameUpdated(UpdateUserTitleDisplayNameResult result) =>
            PlayFabAuthenticateDone();

        #endregion

        private void PlayFabAuthenticateDone()
        {
            IsReady = true;
            ExternalServices.Instance.AuthenticationDone();
        }

        #endregion
    }
}