using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils;
using Photon.Pun;
using Projectiles.Bonuses;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        public PhotonView PhotonView { get; set; }
        public event Action CrossDome;

        public IDamageable Interactor { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionValidator.ValidateCollision<IObstacle>(collision))
                Interactor.Die(true);

            if (CollisionValidator.ValidateCollision(collision, out Bullet bullet))
            {
                if (!Equals(bullet.Data.Owner, PhotonView.Owner))
                    Interactor.TakeDamage(bullet.Data.ScriptableData.Damage, bullet.Data.Owner);
            }

            if (CollisionValidator.ValidateCollision(collision, out HeartBonus bonus))
                Interactor.AddHealth(bonus.BonusHealth);
        }

        private void OnTriggerExit(Collider other)
        {
            CrossDome?.Invoke();
        }
    }
}