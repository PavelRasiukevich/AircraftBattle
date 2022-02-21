using Assets.Scripts.Utils.Timers;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements
{
    public class BattleClock : MonoBehaviour
    {
        public event Action TimeIsOver;

        [SerializeField] private float _time;

        public string FormatedTime => FormatTime((int)_matchTimer.TimeAmmount);

        private MatchTimer _matchTimer;

        private void Awake()
        {
            _matchTimer = new MatchTimer(_time);
        }

        private void Update()
        {
            ClockRun();
        }

        private void ClockRun()
        {
            if (!_matchTimer.IsStopped)
                _matchTimer.Tick(Time.deltaTime);
            else
                TimeIsOver?.Invoke();
        }

        private string FormatTime(int time)
        {
            var min = time / 60;
            var sec = time % 60;

            return $"{min}:{string.Format("{0:d2}", sec)}";
        }
    }
}