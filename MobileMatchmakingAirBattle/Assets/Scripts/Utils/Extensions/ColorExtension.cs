using UnityEngine;

namespace Utils.Extensions
{
    public static class ColorExtension
    {
        public static Vector3 ToVector3(this Color c)
        {
            return new Vector3(c.r, c.g, c.b);
        }
    }
}