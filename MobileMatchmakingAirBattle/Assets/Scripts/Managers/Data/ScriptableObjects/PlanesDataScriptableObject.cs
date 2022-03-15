using System.Collections.Generic;
using TO;
using UnityEngine;

namespace Managers.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlanesData", menuName = "Data/Planes", order = 1)]
    public class PlanesDataScriptableObject : ScriptableObject 
    {
        [SerializeField] private List<PlaneInfo> _planeList;

        public List<PlaneInfo> PlaneList => _planeList;

        public void GetPlaneBy(int id, out PlaneInfo plane)
        {
            plane = null;
            plane = PlaneList.FindLast(p => p.ID == id);
        }
    }
}