using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        private AirCraft AirCraft { get; set; }

        private void Awake()
        {
            AirCraft = GetComponent<AirCraft>();
        }

        private void OnCollisionEnter(Collision other)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Const.Tags.FightArena))
                AirCraft.Data.IsControllable = true;
        }
    }
}