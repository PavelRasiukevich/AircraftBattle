using UnityEngine;

namespace Core
{
    public abstract class BaseInstance : MonoBehaviour
    {
        protected abstract void Awake();
    }

    public abstract class BaseInstance<T> : BaseInstance where T : BaseInstance
    {
        public static T Instance { get; private set; }

        protected override void Awake()
        {
            if (Instance == null)
                Instance = this as T;
        }
    }
}