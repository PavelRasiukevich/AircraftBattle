using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils.Enums;
using UnityEngine;

namespace Assets.Scripts.SriptableObjects
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Create Projectile Data")]
    public class ProjectileDataScriptable : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        [SerializeField] private Projectiles.Flare _flare;
        [SerializeField] private Shell _shell;
        [SerializeField] private BulletType _type;

        public int Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;

        public Projectiles.Flare Flare => _flare;
        public Shell Shell => _shell;
        public BulletType Type => _type;
    }
}