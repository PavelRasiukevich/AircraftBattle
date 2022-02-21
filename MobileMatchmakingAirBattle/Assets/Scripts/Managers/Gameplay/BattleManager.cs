using Assets.Scripts.Core;
using Assets.Scripts.Utils.Timers;
using System.Collections;
using UnityEngine;
using Utils.Enums;

namespace Managers.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        private AirCraftCreator Creator { get; set; }

        private MatchTimer _matchTimer;

        #region UNITY

        void Awake()
        {
            _matchTimer = new MatchTimer(600);

            Creator = GetComponent<AirCraftCreator>();
        }

        void Start()
        {
            GameStart();
        }

        private void Update()
        {
            if (!_matchTimer.IsStopped)
                _matchTimer.Tick(Time.deltaTime);
            else
            {
                //stop game
                //show stats
                //show exit to menu button

                GameFinish();
            }
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