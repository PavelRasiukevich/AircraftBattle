using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Core;
using Interfaces.Subscriber;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TO;
using UnityEngine;
using Utils.Enums;

namespace Managers.External.PlayFabServices.Services
{
    /*
     * Авторизация PlayFab, загрузка информации из PlayFab
     */
    public class PlayFabAuthenticate
    {
        public string PlayFabPlayerId { get; private set; } = "";
        public bool IsReady { get; private set; }

        #region Authenticate

        public void AuthenticateWithCustomId()
        {
            PlayFabSettings.TitleId = Const.PlayFab.TitleId;
            User.Common.Name = $"PlayFab_PLayer_{Random.Range(0, 100)}";
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = PlayFabSettings.DeviceUniqueIdentifier
            };
            PlayFabClientAPI.LoginWithCustomID(
                request,
                RequestPhotonToken,
                ScreenEventHolder.Inst.UnexpectedErrorUI
            );
        }

        public void LoginWithPlayFab(string userName, string userPass)
        {
            PlayFabSettings.TitleId = Const.PlayFab.TitleId;
            User.Common.Name = userName;
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest
            {
                Username = userName,
                Password = userPass
            };
            PlayFabClientAPI.LoginWithPlayFab(
                request,
                RequestPhotonToken,
                error=>
                EventBus.InvokeEvent<IPlayFabErrorHandler>(h =>h.Error(error))
            );
        }

        public void RegisterWithPlayFab(string userName, string userPass, string mail)
        {
            PlayFabSettings.TitleId = Const.PlayFab.TitleId;
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
            {
                Username = userName,
                Password = userPass,
                Email = mail
            };
            PlayFabClientAPI.RegisterPlayFabUser(
                request,
                res => ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen(),
                error => EventBus.InvokeEvent<IPlayFabErrorHandler>(h => h.Error(error))
                );
        }

        public void LoginWithGoogle(string serverAuthCode)
        {
            PlayFabSettings.TitleId = Const.PlayFab.TitleId;
            User.Common.Name = Social.localUser.userName;
            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                ServerAuthCode = serverAuthCode,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithGoogleAccount(
                request,
                RequestPhotonToken,
                ScreenEventHolder.Inst.UnexpectedErrorUI
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
                ScreenEventHolder.Inst.UnexpectedErrorUI
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
                OnStatisticsRequest,
                ScreenEventHolder.Inst.UnexpectedErrorUI
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
                ScreenEventHolder.Inst.UnexpectedErrorUI
            );
        }

        private void OnPlayerProfileRequest(GetPlayerProfileResult result)
        {
            if (string.IsNullOrEmpty(result.PlayerProfile.DisplayName) ||
                result.PlayerProfile.DisplayName != User.Common.Name)
                UpdateUserDisplayName(User.Common.Name);
            else
                LoadInventory();
        }

        #endregion

        #region Update User DisplayName

        private void UpdateUserDisplayName(string displayName)
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
                {
                    DisplayName = displayName
                },
                result => LoadInventory(),
                ScreenEventHolder.Inst.UnexpectedErrorUI);
        }

        #endregion

        #region Load Inventory

        private void LoadInventory()
        {
            int goldCount;
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
                result =>
                {
                    result.VirtualCurrency.TryGetValue(Const.Currencies.Gold, out goldCount);
                    User.Currency.CountUpdate(goldCount);
                    PlayFabAuthenticateDone();
                },
                ScreenEventHolder.Inst.UnexpectedErrorUI
            );
        }

        #endregion

        private void PlayFabAuthenticateDone()
        {
            IsReady = true;
            ExternalServices.Inst.AuthenticationDone();
        }

        #endregion
    }
}