using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils.Enums;
using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Transform _shellSpot;

        [SerializeField] private Gun _gun1;
        [SerializeField] private Gun _gun2;

        public InputSystemHandler InputHandler { get; set; }

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }

        public ReloadTimer ReloadTimer { get; private set; }

        private Bullet _bullet;
        public Bullet Bullet => Resources.Load<Bullet>($"Bullets/{BulletType.Default}");

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

            _gun1.FireBullet(Bullet, PhotonView.Owner, DataModel);
            _gun2.FireBullet(Bullet, PhotonView.Owner, DataModel);

            //   bullet1.Data.Lag = Mathf.Abs(lag);

            Instantiate(Bullet.Data.ScriptableData.Shell, _shellSpot.position, _shellSpot.rotation);
        }

        #endregion
    }
}