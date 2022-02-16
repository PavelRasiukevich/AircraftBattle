using Assets.Scripts.AirCrafts;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils;
using Photon.Pun;
using System;
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

        private BaseTimer _timer;

        private void Start()
        {
            _timer = new BaseTimer(DataModel.ReloadTime);
        }

        private void Update()
        {
            if (InputHandler.InputParams.IsFiring)
                Attack();

            _timer.Tick(Time.deltaTime);
           
        }

        public void Attack()
        {
            if (!PhotonView.IsMine) return;

            if (_timer.IsTimerStoped)
            {
                if (PhotonView.IsMine)
                    PhotonView.RPC(nameof(Attack), RpcTarget.All);

                _timer.ResetTimer();
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