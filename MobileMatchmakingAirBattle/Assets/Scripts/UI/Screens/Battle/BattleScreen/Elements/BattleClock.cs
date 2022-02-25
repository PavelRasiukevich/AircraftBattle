using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements
{
    public class BattleClock : MonoBehaviour
    {
        public event Action TimeIsOver;

        public const int MatchDuration = 600;

        private PhotonView _photonView;

        public string FormatedTime => FormatTime((int)_matchTimer.TimeAmmount);

        private MatchTimer _matchTimer;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _matchTimer = new MatchTimer(MatchDuration);
        }

        private void FixedUpdate()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            _photonView.RPC(nameof(ClockRun), RpcTarget.All);
        }

        [PunRPC]
        private void ClockRun()
        {
            if (!_matchTimer.IsStopped)
                _matchTimer.Tick(Time.fixedDeltaTime);
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