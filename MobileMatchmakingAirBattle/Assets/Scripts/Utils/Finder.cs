using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class Finder
    {
        public static bool TryFindOfType<T>(out T obj) where T : Object
        {
            obj = Object.FindObjectOfType<T>();
            return obj != null;
        }
    }
}