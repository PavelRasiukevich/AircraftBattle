using Assets.Scripts.Interfaces;
using Core;
using Interfaces.EventBus;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Threading.Tasks;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InteractionsHandler : MonoBehaviour, IDamageable
    {
        public event Action Died;

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }

        public Transform View { get; set; }

        public void Die()
        {
            Died?.Invoke();

            foreach (Transform v in View)
            {
                v.SetParent(null);
            }

            StartCoroutine(nameof(Delay));
        }

        public void TakeDamage(int value, Player owner)
            => PhotonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, value, owner);

        [PunRPC]
        private void RPC_TakeDamage(object[] values)
        {
            if (!PhotonView.IsMine) return;

            DataModel.CurrentHp -= (int)values[0];

            if (DataModel.CurrentHp <= 0)
                Die();
            else
                EventBus.InvokeEvent<IBattleScreenEvents>(x => x.DamageUI(DataModel));
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(3);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}