using Assets.Scripts.AirCrafts;
using Assets.Scripts.Projectiles;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        private AirCraft Aircraft { get; set; }

        //add CustomTimer Class 
        [SerializeField] private float _reloadTime;
        private float _elapsedTime;

        private bool _processingFire;

        public PhotonView PhotonView { get; set; }

        private void Awake()
        {
            _elapsedTime = _reloadTime;
            Aircraft = GetComponent<AirCraft>();
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
        }

        public void Attack()
        {
            if (!Aircraft.Data.IsControllable) return;
            if (!PhotonView.IsMine) return;

            if (_elapsedTime >= _reloadTime)
            {
                if (PhotonView.IsMine)
                    PhotonView.RPC(nameof(Attack), RpcTarget.All);
                _elapsedTime = 0;
            }
        }

        #region RPCs

        [PunRPC]
        private void Attack(PhotonMessageInfo info)
        {
            float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

            var bullet = Instantiate(_bulletPrefab, _fireSpot.position, _fireSpot.transform.rotation);

            bullet.Data.Owner = PhotonView.Owner;
            bullet.Data.Lag = Mathf.Abs(lag);
        }

        #endregion
    }
}