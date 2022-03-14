using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _maxRotationAngle;

        private Rigidbody _shellRigidBody;

        private void Awake()
        {
            _shellRigidBody = GetComponent<Rigidbody>();
            DisposeShell();
            ApplyRandomRotation();
            Destroy(gameObject, 1.5f);
        }

        private void ApplyRandomRotation()
        {
            transform.rotation = Quaternion.Euler(Rand(), Rand(), Rand());
        }

        private float Rand()
        {
            return UnityEngine.Random.Range(0.0f, _maxRotationAngle);
        }

        private void DisposeShell()
        {
            _shellRigidBody.AddForce(transform.right * _force, ForceMode.Impulse);
        }
    }
}