using Assets.Scripts.AirCrafts;
using Assets.Scripts.Projectiles;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        public AirCraft AirCraft { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();

            if(bullet != null)
            {
                AirCraft.TakeDamage(10, bullet.Owner);
            }
        }
    }
}