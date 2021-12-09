using System.Collections;
using Assets.Scripts.Core;
using Core;
using Network.External.GooglePlayServices;
using Network.External.PlayFabServices;
using Photon.Pun;
using TO;
using UnityEngine;
using Utils.Enums;

namespace Network.External
{
    /*
     * Сторонние сервисы
     * (GooglePlay, PlayFab)
     */
    public class ExternalServices : BaseInstance<ExternalServices>
    {
        [SerializeField] private AuthenticationType _authenticationType = AuthenticationType.None;
        public BaseGooglePlay GooglePlay { get; set; }
        public BasePlayFab PlayFab { get; set; }

        #region UNITY

        protected override void Awake()
        {
            base.Awake();
            GooglePlay = new BaseGooglePlay();
            PlayFab = new BasePlayFab();
            _authenticationType = Application.platform == RuntimePlatform.Android
                ? AuthenticationType.Google
                : _authenticationType;
        }

        #endregion

        #region PUBLIC

        /*
         * Запуск авторизации
         */
        public void Authentication()
        {
            switch (_authenticationType)
            {
                case AuthenticationType.None:
                    AuthenticationDone();
                    break;
                case AuthenticationType.PlayFabWithLogin:
                    ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen();
                    break;
                case AuthenticationType.PlayFabWithCustomId:
                    PopupHolder.CurrentPopup(PopupType.Loading).Show();
                    PlayFab.Authenticate.AuthenticateWithCustomId();
                    break;
                case AuthenticationType.Google:
                    PopupHolder.CurrentPopup(PopupType.Loading).Show();
                    GooglePlay.Authenticate.SignIn();
                    break;
            }
        }

        /*
         * Авторизация прошла успешно. (Точка входа в меню)
         */
        public void AuthenticationDone()
        {
            StartCoroutine(nameof(LoadDataAndOpenMenu));
        }

        #endregion

        #region PRIVATE

        /*
         * Загрузка всей информации перед запуском меню
         */
        private IEnumerator LoadDataAndOpenMenu()
        {
            switch (_authenticationType)
            {
                case AuthenticationType.None:
                    User.Name = $"Photon_PLayer_{Random.Range(0, 100)}";
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PunAvatar");
                    break;
                case AuthenticationType.PlayFabWithCustomId:
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabAvatar");
                    break;
                case AuthenticationType.PlayFabWithLogin:
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabAvatar");
                    break;
                case AuthenticationType.Google:
                    while (Social.localUser.image == null)
                        yield return null;
                    Texture2D avatar = Social.localUser.image;
                    User.Sprite = Sprite.Create(avatar,
                        new Rect(0.0f, 0.0f, avatar.width, avatar.height),
                        Vector2.zero);
                    break;
            }

            PhotonNetwork.NickName = User.Name;
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        #endregion
    }
}