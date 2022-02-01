using System.Collections.Generic;
using TO;
using UnityEngine;

namespace Managers.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlanesData", menuName = "Data/Planes", order = 1)]
    public class PlanesDataScriptableObject : ScriptableObject
    {
        [SerializeField] private int _currPlaneID;
        [SerializeField] private List<PlaneInfo> _planeList;

        public int CurrID
        {
            get => _currPlaneID;
            set => _currPlaneID = value;
        }

        public List<PlaneInfo> PlaneList => _planeList;

        public bool GetPlaneBy(int id, out PlaneInfo plane)
        {
            plane = null;
            plane = PlaneList.FindLast(p => p.ID == id);
            return plane != null;
        }

        public void ChangePlaneBy(PlaneInfo plane)
        {
            int id = plane.ID;
            int index = PlaneList.FindLastIndex(p => p.ID == id);
            if (index == -1) return;
            PlaneList[index] = plane;
        }
    }
}