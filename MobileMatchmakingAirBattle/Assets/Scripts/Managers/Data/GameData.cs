using Core;
using Core.Base;
using Interfaces.Subscriber;
using Managers.Data.ScriptableObjects;
using TO;
using UnityEngine;

namespace Managers.Data
{
    public class GameData : BaseInstance<GameData>
    {
        [SerializeField] private PlanesDataScriptableObject _planes;

        public PlanesDataScriptableObject Planes => _planes;

        private PlaneInfo _currentPlane;
        public PlaneInfo CurrentPlane => _currentPlane;

        private PlaneInfo _currentShopPlane;
        public PlaneInfo CurrentShopPlane => _currentShopPlane;

        #region Unity

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SelectPlane(Planes.CurrID);
            SelectShopPlane(Planes.CurrID);
        }

        #endregion


        #region PUBLIC

        public void SelectPlane(int id)
        {
            Planes.CurrID = id == 0 ? Planes.PlaneList[0].ID : id;
            Planes.GetPlaneBy(Planes.CurrID, out _currentPlane);
            EventBus.InvokeEvent<IShopRefreshHandler>(h => h.Refresh());
            Planes.CurrID = Planes.CurrID;
        }

        public void SelectShopPlane(int id) =>
            Planes.GetPlaneBy(id, out _currentShopPlane);

        public void ChangeInfoBy(PlaneInfo planeInfo)
        {
            Planes.ChangePlaneBy(planeInfo);
            if (planeInfo.ID == _currentPlane.ID)
                _currentPlane = planeInfo;
            if (planeInfo.ID == _currentShopPlane.ID)
                _currentShopPlane = planeInfo;
        }

        #endregion
    }
}