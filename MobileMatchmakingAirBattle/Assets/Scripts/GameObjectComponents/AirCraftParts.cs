using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class AirCraftParts : MonoBehaviour
    {
        [SerializeField] private List<Transform> _parts;

        public List<Transform> Parts => _parts;
    }
}