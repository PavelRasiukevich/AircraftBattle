using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Timers;
using Managers.Data;
using Photon.Pun;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class BulletGetter
    {
        public Bullet GetBullet() =>
            Resources.Load<Bullet>($"{Const.BulletPath}{GameDataManager.Inst.CurrentPlane.Settings.BulletType}");
    }

    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Transform _shellSpot;

        [SerializeField] private Gun _gun1;
        [SerializeField] private Gun _gun2;

        public InputSystemHandler InputHandler { get; set; }

        public PhotonView PhotonView { get; set; }

        public AircraftDataModel DataModel { get; set; }

        public ReloadTimer ReloadTimer { get; private set; }

        public Bullet Bullet { get; private set; }

        private void Start()
        {
            ReloadTimer = new ReloadTimer(DataModel.ReloadTime);

            Bullet = new BulletGetter().GetBullet();
            
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
                    PhotonView.RPC(nameof(RPC_Attack), RpcTarget.All);

                ReloadTimer.ResetTimer();
            }
        }

        #region RPCs

        [PunRPC]
        private void RPC_Attack(PhotonMessageInfo info)
        {
            _gun1.FireBullet(Bullet, PhotonView.Owner, DataModel);
            _gun2.FireBullet(Bullet, PhotonView.Owner, DataModel);

            Instantiate(Bullet.Data.ScriptableData.Shell, _shellSpot.position, _shellSpot.rotation);
        }

        #endregion
    }
}