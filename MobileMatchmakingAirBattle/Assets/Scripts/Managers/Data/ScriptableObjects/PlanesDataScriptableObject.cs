using System.Collections.Generic;
using TO;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlanesData", menuName = "Data/Planes", order = 1)]
    public class PlanesDataScriptableObject : ScriptableObject
    {
        public List<PlaneInfo> _planeList;

    }
}