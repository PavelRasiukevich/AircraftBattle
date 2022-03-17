using System;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using UnityEngine;

namespace UI.Screens.Battle.BattleScreen.Elements
{
    public class BattleClock : MonoBehaviour
    {
        public event Action OnTimeIsOver;


        private PhotonView PhotonView { get; set; }
        public string FormatedTime => FormatTime((int) _matchTimer.TimeAmmount);

        private MatchTimer _matchTimer;

        public void Config(PhotonView photonView)
        {
            PhotonView = photonView;
            _matchTimer = new MatchTimer(Const.Conditions.MatchDuration);
        }

        private void FixedUpdate()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            PhotonView.RPC(nameof(ClockRun), RpcTarget.All);
        }

        [PunRPC]
        private void ClockRun()
        {
            if (!_matchTimer.IsStopped)
                _matchTimer.Tick(Time.fixedDeltaTime);
            else
                OnTimeIsOver?.Invoke();
        }

        private string FormatTime(int time)
        {
            var min = time / 60;
            var sec = time % 60;

            return $"{min}:{string.Format("{0:d2}", sec)}";
        }
    }
}