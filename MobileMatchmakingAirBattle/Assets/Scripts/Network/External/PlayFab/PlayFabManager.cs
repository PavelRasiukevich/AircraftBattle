using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UI.Screens;
using Utils.Enums;

namespace Network.External.PlayFab
{
    /*
     * Управление PlayFab
     */
    public class PlayFabManager
    {
        public string PlayFabPlayerId { get; private set; } = "";
        public string PlayFabPlayerName { get; private set; } = "";

        #region public Authenticate

        public void AuthenticateWithCustomId()
        {
            PlayFabSettings.TitleId = UtilsConst.TitleId;
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = PlayFabSettings.DeviceUniqueIdentifier
            };
            PlayFabClientAPI.LoginWithCustomID(
                request,
                RequestPhotonToken,
                err => ScreenEventHolder.Instance.UnexpectedError(err.ErrorMessage)
            );
        }

        public void LoginWithPlayFab(string userName, string userPass)
        {
            PlayFabSettings.TitleId = UtilsConst.TitleId;
            PlayFabPlayerName = userName;
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
            PlayFabSettings.TitleId = UtilsConst.TitleId;
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
            PlayFabSettings.TitleId = UtilsConst.TitleId;
            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                ServerAuthCode = serverAuthCode,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithGoogleAccount(
                request,
                RequestPhotonToken,
                err => ScreenEventHolder.Instance.UnexpectedError(err.ErrorMessage)
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
                err => ScreenEventHolder.Instance.UnexpectedError(err.ErrorMessage)
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
            ExternalServicesManager.Instance.AuthenticationDone();
        }

        #endregion

        #endregion
    }
}