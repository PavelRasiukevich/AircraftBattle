using System.Collections;
using Assets.Scripts.Core;
using Core;
using Network.External.Google;
using Network.External.PlayFab;
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
        public GooglePlayAuthenticate GooglePlayAuthenticate { get; private set; }
        public PlayFabAuthenticate PlayFabAuthenticate { get; private set; }
        public PlayFabLeaderboards PlayFabLeaderboards { get; private set; }
        public PlayFabStatistics PlayFabStatistics { get; private set; }


        #region UNITY

        protected override void Awake()
        {
            base.Awake();
            GooglePlayAuthenticate = new GooglePlayAuthenticate();
            PlayFabAuthenticate = new PlayFabAuthenticate();
            PlayFabLeaderboards = new PlayFabLeaderboards();
            PlayFabStatistics = new PlayFabStatistics();
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
                    PlayFabAuthenticate.AuthenticateWithCustomId();
                    break;
                case AuthenticationType.Google:
                    PopupHolder.CurrentPopup(PopupType.Loading).Show();
                    GooglePlayAuthenticate.SignIn();
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
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PunLogo");
                    break;
                case AuthenticationType.PlayFabWithCustomId:
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabLogo");
                    break;
                case AuthenticationType.PlayFabWithLogin:
                    User.Sprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabLogo");
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