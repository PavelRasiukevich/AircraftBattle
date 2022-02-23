using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements
{
    public class BattleClock : MonoBehaviour
    {
        public event Action TimeIsOver;

        public const int MatchDuration = 600;

        private PhotonView _photonView;

        public string FormatedTime => FormatTime((int)_matchTimer.TimeAmmount);

        private MatchTimer _matchTimer;

        private Actions _actions;

        private bool IsPressed = false;

        private void Awake()
        {

            _actions = new Actions();

            _photonView = GetComponent<PhotonView>();

            _matchTimer = new MatchTimer(MatchDuration);
        }

        private void OnEnable()
        {
            _actions.PUNNetworkTest.ActivateDeactivate.Enable();
            _actions.PUNNetworkTest.ActivateDeactivate.performed += Handler;
        }

        private void Handler(InputAction.CallbackContext obj)
        {
            IsPressed = !IsPressed;
        }

        private void OnDisable()
        {
            _actions.PUNNetworkTest.ActivateDeactivate.Disable();
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            if (!IsPressed)
                _photonView.RPC(nameof(ClockRun), RpcTarget.All);
        }

        [PunRPC]
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