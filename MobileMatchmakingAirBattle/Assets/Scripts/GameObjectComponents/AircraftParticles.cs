using UnityEngine;

namespace GameObjectComponents
{
    public class AircraftParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyParticle;

        public void DestroyEffect()
        {
            // Spawn effect (RPC)
        }
    }
}