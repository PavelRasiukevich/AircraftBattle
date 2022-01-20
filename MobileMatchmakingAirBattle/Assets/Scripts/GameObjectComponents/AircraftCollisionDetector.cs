using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AircraftCollisionDetector : MonoBehaviour
    {
        public AirCraft AirCraft { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Const.Tags.Ground))
                AirCraft.Die();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Const.Tags.FightArena))
                AirCraft.DataModel.IsControllable = true;
        }
    }
}