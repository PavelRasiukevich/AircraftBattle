using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        public InputSystemHandler InputHandler { get; set; }

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }

        public ReloadTimer ReloadTimer { get; private set; }

        private void Start()
        {
            ReloadTimer = new ReloadTimer(DataModel.ReloadTime);
        }

        private void Update()
        {
            if (InputHandler.InputParams.HasFire)
                Attack();
           
            ReloadTimer.Tick(Time.deltaTime);
        }

        public void Attack()
        {
            if (!PhotonView.IsMine) return;

            if (ReloadTimer.IsStopped)
            {
                if (PhotonView.IsMine)
                    PhotonView.RPC(nameof(Attack), RpcTarget.All);

                ReloadTimer.ResetTimer();
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