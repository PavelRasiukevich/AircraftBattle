using UnityEngine;

namespace Assets.Scripts.SriptableObjects
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Create Projectile Data")]
    public class ProjectileDataScriptable : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        public int Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;
    }
}