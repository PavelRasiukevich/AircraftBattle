using Assets.Scripts.Utils;
using Core;
using Interfaces.EventBus.PlayerProperties;
using Photon.Pun;
using UnityEngine;
using Utils.Extensions;

namespace UI.Screens.Battle.BattleScreen.Elements.Stats
{
    public class StatsBar : MonoBehaviour, IStatsUpdate
    {
        [SerializeField] private StatsLine _statLinePrefab;

        void OnEnable()
        {
            Refresh();
            EventBus.AddListener<IStatsUpdate>(this);
        }

        private void OnDisable()
        {
            EventBus.RemoveListener<IStatsUpdate>(this);
        }

        private void Refresh()
        {
            foreach (var line in transform.GetComponentsInChildren<StatsLine>())
                Destroy(line.gameObject);
            foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                StatsLine line = Instantiate(_statLinePrefab, transform);
                line.Config(
                    player.ActorNumber,
                    player.NickName,
                    player.GetPropertyValue(Const.Properties.Frags, 0),
                    player.GetPropertyValue(Const.Properties.Fails, 0)
                );
            }
        }

        public void OnFailsChanged(int actorNumber, int fails)
        {
            foreach (var line in transform.GetComponentsInChildren<StatsLine>())
                if (line.ActorNumber == actorNumber)
                    line.ConfigFail((int) fails);
        }

        public void OnFragsChanged(int actorNumber, int frags)
        {
            foreach (var line in transform.GetComponentsInChildren<StatsLine>())
                if (line.ActorNumber == actorNumber)
                    line.ConfigFrags((int) frags);
        }
    }
}