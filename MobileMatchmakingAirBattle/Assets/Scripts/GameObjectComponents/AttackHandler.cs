using Assets.Scripts.AirCrafts;
using Assets.Scripts.Projectiles;
using Assets.Scripts.UI.JoyStick;
using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        public AirCraft Aircraft { get; set; }

        //add CustomTimer Class 
        [SerializeField] private float _reloadTime;
        private float _elapsedTime;

        private bool _processingFire;

        public PhotonView PhotonView { get; set; }

        private void Awake()
        {
            _elapsedTime = _reloadTime;
        }

        public void Attack(bool isFiring)
        {
            if (!Aircraft.DataModel.IsControllable) return;
            if (!PhotonView.IsMine) return;

            if (isFiring)
            {
                if (_elapsedTime >= _reloadTime)
                {
                    if (PhotonView.IsMine)
                        PhotonView.RPC(nameof(Attack), RpcTarget.All);

                    _elapsedTime = 0.0f;
                }

                _elapsedTime += Time.deltaTime;
            }
            else
            {
                if (_elapsedTime < _reloadTime)
                    _elapsedTime += Time.deltaTime;
                else
                    _elapsedTime = _reloadTime;
            }
        }

        #region RPCs

        [PunRPC]
        private void Attack(PhotonMessageInfo info)
        {

            float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

            var bullet = Instantiate(_bulletPrefab, _fireSpot.position, _fireSpot.transform.rotation);

            bullet.BulletDataModel.Owner = PhotonView.Owner;
            bullet.BulletDataModel.Lag = Mathf.Abs(lag);
        }

        #endregion
    }
}