using Assets.Scripts.Interfaces;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        public Player Owner { get; set; }
        public float Lag { get; set; }

        [SerializeField] private int _speed;

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();

            Destroy(gameObject, 5.0f);
        }

        private void FixedUpdate()
        {
            _rigidBody.AddRelativeForce(Vector3.forward * _speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                Destroy(gameObject);
            }
        }
    }
}