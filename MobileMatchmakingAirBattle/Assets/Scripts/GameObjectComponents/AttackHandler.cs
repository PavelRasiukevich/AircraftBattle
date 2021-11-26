using Assets.Scripts.Projectiles;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        public PhotonView PhotonView { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PhotonView.IsMine)
                    PhotonView.RPC(nameof(Attack), RpcTarget.All);
            }
        }

        #region RPCs

        [PunRPC]
        private void Attack(PhotonMessageInfo info)
        {
           // float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTimestamp));

            // info.photonView.Owner
            var bullet = Instantiate(_bulletPrefab, _fireSpot.position, _fireSpot.transform.rotation);
            bullet.Owner = PhotonView.Owner;
            //bullet.Lag = lag;
        }

        #endregion
    }
}