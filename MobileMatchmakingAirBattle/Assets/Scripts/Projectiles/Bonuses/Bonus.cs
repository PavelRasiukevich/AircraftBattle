using Assets.Scripts.Audio;
using Photon.Pun;
using UnityEngine;

namespace Projectiles.Bonuses
{
    public class Bonus : MonoBehaviourPunCallbacks
    {
        private PhotonView _photonView;
        [SerializeField] private GameObject _bonus;
        [SerializeField] private float _bonusTime = 30;
        private float _time = 0;
        private bool _isActive = false;

        void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        void FixedUpdate()
        {
            if (_bonus.activeSelf) return;
            _time += Time.fixedDeltaTime;
            if (_time >= _bonusTime)
            {
                _time = 0;
                SpawnBonus();
            }
        }

        private void SpawnBonus()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            _isActive = true;
            _photonView.RPC(nameof(RPC_SpawnBonus), RpcTarget.All);
        }

        [PunRPC]
        private void RPC_SpawnBonus()
        {
            _bonus.SetActive(true);
        }

        public void RemoveBonus()
        {
            if (!_isActive) return;
            _isActive = false;
            _photonView.RPC(nameof(RPC_RemoveBonus), RpcTarget.All);
        }

        [PunRPC]
        public void RPC_RemoveBonus()
        {
            _bonus.SetActive(false);

            AudioController.Instance.PlaySound(SoundName.Powerup.ToString(), gameObject);
        }
    }
}