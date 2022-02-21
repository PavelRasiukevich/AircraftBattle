using Assets.Scripts.Interfaces;
using Assets.Scripts.SriptableObjects;
using TO;
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

        public AircraftDataModel AirCraftDataModel { get; set; }

        #region UNITY

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();

            Destroy(gameObject, Data.Data.LifeTime);
        }

        private void FixedUpdate()
        {
            _rigidBody.AddRelativeForce(Vector3.forward * (AirCraftDataModel.Speed.MoveSpeed * 2), ForceMode.Impulse);
            _rigidBody.velocity += _rigidBody.velocity * Data.Lag;
        }

        #endregion

        #region EVENTS

        private void OnCollisionEnter(Collision collision)
        {
            DestroySelf();
        }

        #endregion

        #region PRIVATE

        private void DestroySelf()
        {
            // Spawn effect
            Destroy(gameObject);
        }

        #endregion
    }
}