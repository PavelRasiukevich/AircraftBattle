using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        public event Action CrossDome;

        public IDamageable Interactor { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionValidator.ValidateCollision<IObstacle>(collision))
                Interactor.Die(true);

            if (CollisionValidator.ValidateCollision(collision, out Bullet sender))
                Interactor.TakeDamage(sender.Data.Data.Damage, sender.Data.Owner);
        }

        private void OnTriggerExit(Collider other)
        {
            CrossDome?.Invoke();
        }
    }
}