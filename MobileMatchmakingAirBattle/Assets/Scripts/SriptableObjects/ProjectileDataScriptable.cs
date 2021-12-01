using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SriptableObjects
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Create Projectile Data")]
    public class ProjectileDataScriptable : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public int Damage => _damage;
        public float Speed => _speed;
    }
}