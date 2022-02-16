using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class CollisionValidator
    {
        public static bool ValidateCollision<T>(Collision collision, out T component)
        {
            return collision.gameObject.TryGetComponent(out component);
        }

        public static bool ValidateCollision<T>(Collision collision)
        {
            return collision.gameObject.TryGetComponent(out T _);
        }

        public static bool ValidateTrigger<T>(Collider trigger, out T component)
        {
            return trigger.gameObject.TryGetComponent(out component);
        }
    }
}