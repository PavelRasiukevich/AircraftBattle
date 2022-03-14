using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils.Timers;
using Photon.Pun;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Shell _shell;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private GameObject _flare;

        [SerializeField] private Transform _fireSpot1;
        [SerializeField] private Transform _fireSpot2;
        [SerializeField] private Transform _flareSpot1;
        [SerializeField] private Transform _flareSpot2;
        [SerializeField] private Transform _shellSpot;

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

            var bullet1 = Instantiate(_bullet, _fireSpot1.position, _fireSpot1.transform.rotation);
            var bullet2 = Instantiate(_bullet, _fireSpot2.position, _fireSpot2.transform.rotation);

            bullet1.AirCraftDataModel = DataModel;
            bullet1.Data.Owner = PhotonView.Owner;
            bullet1.Data.Lag = Mathf.Abs(lag);

            bullet2.AirCraftDataModel = DataModel;
            bullet2.Data.Owner = PhotonView.Owner;
            bullet2.Data.Lag = Mathf.Abs(lag);

            PhotonView.RPC(nameof(InstantiateShell), RpcTarget.All);
            PhotonView.RPC(nameof(InstantiateFlare), RpcTarget.All);
        }

        [PunRPC]
        private void InstantiateShell(PhotonMessageInfo info) => Instantiate(_shell, _shellSpot.position, _shellSpot.rotation);

        [PunRPC]
        private void InstantiateFlare()
        {
            Instantiate(_flare, _flareSpot1.position, _flareSpot1.rotation);
            Instantiate(_flare, _flareSpot2.position, _flareSpot2.rotation);
        }

        #endregion
    }
}