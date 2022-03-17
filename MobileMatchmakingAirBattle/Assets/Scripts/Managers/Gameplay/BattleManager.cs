using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Enums;
using Interfaces.EventBus.PlayerProperties;
using Photon.Pun;
using UI.Screens.Battle.BattleScreen.Elements;
using UnityEngine;

namespace Managers.Gameplay
{
    public class BattleManager : MonoBehaviour, IStatsUpdate
    {
        private BattleClock BattleTimer { get; set; }
        private PhotonView PhotonView { get; set; }
        private AirCraftCreator Creator { get; set; }
        private BattleState BattleState { get; set; }

        #region UNITY

        void Awake()
        {
            Creator = GetComponent<AirCraftCreator>();
            PhotonView = GetComponent<PhotonView>();
            BattleTimer = GetComponent<BattleClock>();
            BattleTimer.Config(PhotonView);
        }

        private void OnEnable()
        {
            BattleTimer.OnTimeIsOver += GameFinish;
        }

        private void OnDisable()
        {
            BattleTimer.OnTimeIsOver -= GameFinish;
        }

        void Start()
        {
            GameStart();
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
            if (BattleState == BattleState.Finish) return;
            BattleState = BattleState.Wait;
            StartCoroutine(nameof(Wait));
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFail).ShowScreen();
        }

        private void GameStart()
        {
            if (BattleState == BattleState.Finish) return;
            BattleState = BattleState.Battle;
            Creator.Create();
            ScreenHolder.SetCurrentScreen(ScreenType.Battle).ShowScreen();
        }

        private void GameFinish() => PhotonView.RPC(nameof(RPC_Finish), RpcTarget.All);

        [PunRPC]
        private void RPC_Finish()
        {
            if (!PhotonView.IsMine) return;

            BattleState = BattleState.Finish;
            Creator.Destroy();
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFinish).ShowScreen();
        }

        #endregion

        public void OnFailsChanged(int actorNumber, int fails)
        {
        }

        public void OnFragsChanged(int actorNumber, int frags)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == actorNumber && frags >= Const.Conditions.FragsToWin)
                GameFinish();
        }
    }
}