using Assets.Scripts.Interfaces;
using Core;
using Interfaces.EventBus;
using Photon.Pun;
using Photon.Realtime;
using System;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InteractionsHandler : MonoBehaviour, IDamageable
    {
        public event Action Died;

        [SerializeField] private GameObject _effect;

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }


        public void Die()
        {
            Died?.Invoke();
            PhotonView.RPC(nameof(CreateDestroyEffect), RpcTarget.All);

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