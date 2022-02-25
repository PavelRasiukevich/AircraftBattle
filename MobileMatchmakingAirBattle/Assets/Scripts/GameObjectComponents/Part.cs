using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class Part : MonoBehaviour, IPart
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void DestroyPart()
        {
            Destroy(gameObject);
        }

        public void DisableColliders()
        {
            _collider.enabled = false;
        }
    }
}