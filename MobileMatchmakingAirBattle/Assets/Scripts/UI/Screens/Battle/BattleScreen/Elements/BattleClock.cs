using Assets.Scripts.Utils.Timers;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements
{
    public class BattleClock : MonoBehaviour
    {
        public event Action TimeIsOver;

        [SerializeField] private float _time;

        private TextMeshProUGUI _textMesh;

        private MatchTimer _matchTimer;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();

            _matchTimer = new MatchTimer(_time);
        }

        private void Update()
        {
            if (!_matchTimer.IsStopped)
            {
                _matchTimer.Tick(Time.deltaTime);

                UpdateTimerView(ToReadableValue((int)_matchTimer.TimeAmmount));
            }
            else
            {
                TimeIsOver?.Invoke();
            }
        }

        private void UpdateTimerView(string time)
        {
            _textMesh.text = time;
        }

        private string ToReadableValue(int time)
        {
            var min = time / 60;
            var sec = time % 60;

            return $"{min}:{string.Format("{0:d2}", sec)}";
        }
    }
}