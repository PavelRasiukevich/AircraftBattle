using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using Core;
using Interfaces.EventBus;
using Photon.Pun;
using Photon.Realtime;
using TO;
using UnityEngine;
using Utils.Extensions;

namespace Assets.Scripts.GameObjectComponents
{
    public class InteractionsHandler : MonoBehaviour, IDamageable
    {
        public event Action Died;

        [SerializeField] private GameObject _effect;

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }


        public void Die(bool isHit) // true - сбит   false - раунд завершен
        {
            if (!PhotonView.IsMine) return;
            Died?.Invoke();
            PhotonView.RPC(nameof(CreateDestroyEffect), RpcTarget.All);
            if (isHit) PhotonView.Owner.AddValueToProperty(Const.Properties.Fails, 1);
            PhotonNetwork.Destroy(gameObject);
        }

        public void TakeDamage(int value, Player owner)
            => PhotonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, value, owner);

        [PunRPC]
        private void RPC_TakeDamage(object[] values)
        {
            if (!PhotonView.IsMine) return;

            DataModel.CurrentHp -= (int) values[0];

            if (DataModel.CurrentHp <= 0)
            {
                Player player = (Player) values[1];
                player.AddValueToProperty(Const.Properties.Frags, 1);
                Die(true);
            }
            else
                EventBus.InvokeEvent<IBattleScreenEvents>(x => x.DamageUI(DataModel));
        }

        [PunRPC]
        private void CreateDestroyEffect()
        {
            if (!PhotonView.IsMine) return;

            Instantiate(_effect,
                PhotonView.GetComponent<Transform>().position,
                PhotonView.GetComponent<Transform>().rotation
            );
        }
    }
}