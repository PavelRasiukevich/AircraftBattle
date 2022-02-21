using Assets.Scripts.Core;
using Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements;
using System.Collections;
using UnityEngine;
using Utils.Enums;

namespace Managers.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleClock _timer;

        private AirCraftCreator Creator { get; set; }


        #region UNITY

        void Awake()
        {
            Creator = GetComponent<AirCraftCreator>();
        }

        private void OnEnable()
        {
            _timer.TimeIsOver += GameFinish;
        }

        private void OnDisable()
        {
            _timer.TimeIsOver -= GameFinish;
        }

        void Start()
        {
            GameStart();
        }

        private void Update()
        {
           
        }

        #endregion

        #region PRIVATE

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3.0f);
            GameStart();
            StopCoroutine(nameof(Wait));
        }

        public void GameFail()
        {
            StartCoroutine(nameof(Wait));
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFail).ShowScreen();
        }

        private void GameStart()
        {
            Creator.Create();
            ScreenHolder.SetCurrentScreen(ScreenType.Battle).ShowScreen();
        }

        private void GameFinish()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFinish).ShowScreen();
        }

        #endregion
    }
}