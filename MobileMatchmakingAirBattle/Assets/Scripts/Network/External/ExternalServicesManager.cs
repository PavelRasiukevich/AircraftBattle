using System.Collections;
using Assets.Scripts.Core;
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
    public class ExternalServicesManager : MonoBehaviour
    {
        public static ExternalServicesManager Instance { get; private set; }

        [Header("Authenticate Type")] [SerializeField]
        private AuthenticationType _authenticationType = AuthenticationType.None;

        public GooglePlayManager GooglePlayManager { get; private set; }
        public PlayFabManager PlayFabManager { get; private set; }

        void Awake()
        {
            Instance = this;
            GooglePlayManager = new GooglePlayManager();
            PlayFabManager = new PlayFabManager();
            _authenticationType = Application.platform == RuntimePlatform.Android
                ? AuthenticationType.Google
                : _authenticationType;
        }

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
                    PlayFabManager.AuthenticateWithCustomId();
                    break;
                case AuthenticationType.Google:
                    GooglePlayManager.SignIn();
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

        /*
         * Загрузка всей информации перед запуском меню
         */
        private IEnumerator LoadDataAndOpenMenu()
        {
            switch (_authenticationType)
            {
                case AuthenticationType.None:
                    Player.PlayerName = $"Photon_PLayer_{Random.Range(0, 100)}";
                    Player.PlayerSprite = Resources.Load<Sprite>("Sprite/Avatar/PunLogo");
                    break;
                case AuthenticationType.PlayFabWithCustomId:
                    Player.PlayerName = $"PlayFab_PLayer_{Random.Range(0, 100)}";
                    Player.PlayerSprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabLogo");
                    break;
                case AuthenticationType.PlayFabWithLogin:
                    Player.PlayerName = PlayFabManager.PlayFabPlayerName;
                    Player.PlayerSprite = Resources.Load<Sprite>("Sprite/Avatar/PlayFabLogo");
                    break;
                case AuthenticationType.Google:
                    Player.PlayerName = Social.localUser.userName;
                    while (Social.localUser.image == null)
                        yield return null;
                    Player.PlayerSprite = Sprite.Create(Social.localUser.image,
                        new Rect(0.0f, 0.0f, Social.localUser.image.width, Social.localUser.image.height),
                        Vector2.zero);
                    break;
            }

            PhotonNetwork.NickName = Player.PlayerName;
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }
    }
}