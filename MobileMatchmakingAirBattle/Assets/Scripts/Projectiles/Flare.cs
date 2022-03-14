using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Flare : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 0.35f);
        }
    }
}