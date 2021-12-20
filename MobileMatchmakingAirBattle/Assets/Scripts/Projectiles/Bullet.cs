using Assets.Scripts.GameObjectComponents;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        #region DATA
        [SerializeField] private ProjectileDataModel _bulletDataModel;
        #endregion

        #region COMPONENTS
        private Rigidbody _rigidBody;
        #endregion

        public ProjectileDataModel BulletDataModel => _bulletDataModel;

        private void Awake()
        {
            BulletDataModel.Init();

            _rigidBody = GetComponent<Rigidbody>();

            Destroy(gameObject, 5.0f);
        }

        private void FixedUpdate()
        {
            _rigidBody.AddRelativeForce(Vector3.forward * BulletDataModel.Speed, ForceMode.Impulse);
            _rigidBody.velocity += _rigidBody.velocity * BulletDataModel.Lag;
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