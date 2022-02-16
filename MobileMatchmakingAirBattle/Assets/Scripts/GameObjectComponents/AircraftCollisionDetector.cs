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

    }
}