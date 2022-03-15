using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Core;
using Core.Base;
using Managers.External.GooglePlayServices;
using Managers.External.PlayFabServices;
using Photon.Pun;
using TO;
using UnityEngine;
using Utils.Enums;

namespace Managers.External
{
    /*
     * Сторонние сервисы
     * (GooglePlay, PlayFab)
     */
    public class ExternalServices : BaseInstance<ExternalServices>
    {
        [SerializeField] private AuthenticationType _authenticationType = AuthenticationType.PlayFabQuickly;
        public BaseGooglePlay GooglePlay { get; } = new BaseGooglePlay();
        public BasePlayFab PlayFab { get; } = new BasePlayFab();


        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        #region PUBLIC

        /*
         * Запуск авторизации
         */
        public void Authentication()
        {
            _authenticationType = Application.platform == RuntimePlatform.Android
                ? AuthenticationType.PlayFabQuickly // TODO: перед публикацией поставить Google
                : _authenticationType;
            switch (_authenticationType)
            {
                case AuthenticationType.None: // ONLY FOR TEST!
                    User.Common.Name = "Test_User_" + Random.Range(0, 1000);
                    AuthenticationDone();
                    break;
                case AuthenticationType.PlayFabWithLogin:
                    ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen();
                    break;
                case AuthenticationType.PlayFabQuickly:
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
        public void AuthenticationDone() => StartCoroutine(nameof(LoadDataAndOpenMenu));

        #endregion

        #region PRIVATE

        /*
         * Загрузка всей информации, вход в меню
         */
        private IEnumerator LoadDataAndOpenMenu()
        {
            // Аватар
            switch (_authenticationType)
            {
                case AuthenticationType.None:
                case AuthenticationType.PlayFabQuickly:
                case AuthenticationType.PlayFabWithLogin:
                    User.Common.Sprite = Resources.Load<Sprite>(Const.Path.DefaultAvatar);
                    break;
                case AuthenticationType.Google:
                    while (Social.localUser.image == null)
                        yield return null;
                    Texture2D avatar = Social.localUser.image;
                    User.Common.Sprite = Sprite.Create(avatar,
                        new Rect(0.0f, 0.0f, avatar.width, avatar.height),
                        Vector2.zero);
                    break;
            }

            // NickName
            PhotonNetwork.NickName = User.Common.Name;
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        #endregion
    }
}