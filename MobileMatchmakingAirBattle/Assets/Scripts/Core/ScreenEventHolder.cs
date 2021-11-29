using PlayFab;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Screens
{
    /*
     * Вызов событьий влияющих на UI
     * (Добавить UnityEvent, связать в редакторе с public методами)
     */
    public class ScreenEventHolder : MonoBehaviour
    {
        public static ScreenEventHolder Instance { get; private set; }

        [SerializeField] private UnityEvent<string> _unexpectedErrorEvent;
        [SerializeField] private UnityEvent<string> _loginErrorEvent;
        [SerializeField] private UnityEvent<string> _registrationErrorEvent;
        [SerializeField] private UnityEvent<string> _googleErrorEvent;

        #region UNITY

        void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Events

        /*
         * Ошибка авторизации (логин + пароль)
         */
        public void ErrorLogin(PlayFabError error) => _loginErrorEvent.Invoke(error.ErrorMessage);

        /*
         * Ошибка регистрации
         */
        public void ErrorRegistration(PlayFabError error) => _registrationErrorEvent.Invoke(error.ErrorMessage);

        /*
         * Ошибка Google Play
         */
        public void ErrorGooglePlay(string error) => _googleErrorEvent.Invoke(error);

        /*
         * Непредвиденная ошибка
         */
        public void UnexpectedError(string errorText) => _unexpectedErrorEvent.Invoke(errorText);

        #endregion
    }
}