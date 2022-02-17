using Assets.Scripts.Interfaces;
using Core;
using Interfaces.EventBus;
using Photon.Pun;
using Photon.Realtime;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InteractionsHandler : MonoBehaviour, IDamageable 
    {
        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }

        public void Die()
        {
            EventBus.InvokeEvent<IDestroy>(x => x.DestroyAircraft());

            PhotonNetwork.Destroy(gameObject);
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
    }
}