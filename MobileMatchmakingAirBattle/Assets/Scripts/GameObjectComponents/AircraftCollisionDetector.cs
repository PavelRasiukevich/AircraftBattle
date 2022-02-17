using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System;
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
                Interactor.Die();
        }

        private void OnTriggerExit(Collider other)
        {
            CrossDome?.Invoke();
        }
    }
}