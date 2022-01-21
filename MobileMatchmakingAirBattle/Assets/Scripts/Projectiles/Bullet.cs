using Assets.Scripts.Interfaces;
using Assets.Scripts.SriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private ProjectileDataModel _data;
        [SerializeField] private ParticleSystem _destroyPs;
        private Rigidbody _rigidBody;

        #endregion

        public ProjectileDataModel Data => _data;

        #region UNITY

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            Destroy(gameObject, Data.Data.LifeTime);
        }

        private void FixedUpdate()
        {
            _rigidBody.AddRelativeForce(Vector3.forward * Data.Data.Speed, ForceMode.Impulse);
            _rigidBody.velocity += _rigidBody.velocity * Data.Lag;
        }

        #endregion

        #region EVENTS

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target;
            if (collision.gameObject.TryGetComponent<IDamageable>(out target))
                target.TakeDamage(Data.Data.Damage, Data.Owner);
            DestroySelf();
        }

        #endregion

        #region PRIVATE

        private void DestroySelf()
        {
            // Spawn effect RPC
            Destroy(gameObject);
        }

        #endregion
    }
}