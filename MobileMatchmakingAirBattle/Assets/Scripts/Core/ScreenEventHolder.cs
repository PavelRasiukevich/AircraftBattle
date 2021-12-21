using System.Collections.Generic;
using Assets.Scripts.Core;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;
using Utils.Enums;

namespace Core
{
    /*
     * Вызов событьий влияющих на UI
     * (Добавить UnityEvent, связать в редакторе с public методами)
     */
    public class ScreenEventHolder : BaseInstance<ScreenEventHolder>
    {
        [SerializeField] private UnityEvent<string> _loginErrorEvent;
        [SerializeField] private UnityEvent<string> _registrationErrorEvent;
        [SerializeField] private UnityEvent<string> _googleErrorEvent;
        [SerializeField] private UnityEvent<List<PlayerLeaderboardEntry>> _leaderBoardLoadEvent;
        [SerializeField] private UnityEvent _shopEditInfoEvent;

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
         *  Обновить Рейтинг
         */
        public void RefreshLeaderboardLoad(List<PlayerLeaderboardEntry> leaderboard) =>
            _leaderBoardLoadEvent.Invoke(leaderboard);
        
        /*
         * Обновить Магазин
         */
        public void RefreshShop() => _shopEditInfoEvent.Invoke();

        #endregion

        /*
         *  Unexpected Error
         */
        public void UnexpectedErrorUI(PlayFabError err) =>
            UnexpectedErrorUI(err.ErrorMessage);

        private void UnexpectedErrorUI(string err) =>
            PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config(err).Show();
    }
}