using Assets.Scripts.AirCrafts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        public IDamageable Interactor { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionValidator.ValidateCollision<IObstacle>(collision))
                Interactor.Die();
        }
    }
}