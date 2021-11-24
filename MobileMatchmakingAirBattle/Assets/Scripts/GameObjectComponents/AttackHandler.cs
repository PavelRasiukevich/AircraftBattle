using Assets.Scripts.Projectiles;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Transform _fireSpot;
        [SerializeField] private Bullet _bulletPrefab;

        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_photonView.IsMine)
                    _photonView.RPC(nameof(Attack), RpcTarget.All);
            }
        }

        #region RPCs

        [PunRPC]
        private void Attack(PhotonMessageInfo info)
        {
            print($"Info -> {info}");
            var b = Instantiate(_bulletPrefab, _fireSpot.position, Quaternion.identity);
        }

        #endregion
    }
}