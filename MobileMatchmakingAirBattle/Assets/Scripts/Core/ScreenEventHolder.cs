using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;

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
         *  Рейтинг загружен
         */
        public void LeaderboardLoad(List<PlayerLeaderboardEntry> leaderboard) =>
            _leaderBoardLoadEvent.Invoke(leaderboard);

        #endregion
    }
}