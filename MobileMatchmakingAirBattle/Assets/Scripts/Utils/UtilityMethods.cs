using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class UtilityMethods
    {
        public static bool ValidateCollision(Collision collision)
        {
            return collision.gameObject.TryGetComponent(out Obstacle _);
        }
    }
}