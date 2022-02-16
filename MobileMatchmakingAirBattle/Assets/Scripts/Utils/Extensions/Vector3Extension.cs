using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector3Extension
    {
        public static Color ToColor(this Vector3 v)
        {
            return new Color(v.x, v.y, v.z);
        }
    }
}