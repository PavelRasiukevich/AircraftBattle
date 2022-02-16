using Core;
using Core.Base;
using Interfaces.EventBus;
using Managers.Data.ScriptableObjects;
using TO;
using UI.Screens.Shop;
using UnityEngine;

namespace Managers.Data
{
    public class GameDataManager : BaseInstance<GameDataManager>
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
            SelectPlane(Planes.PlaneList[0].ID);
            SelectShopPlane(Planes.PlaneList[0].ID);
        }

        #endregion

        #region PUBLIC

        public void SelectPlane(int id)
        {
            int currId = id == 0 ? Planes.PlaneList[0].ID : id;
            _currentPlane = GetPlane(currId);
            EventBus.InvokeEvent<IShopScreenEvents>(h => h.Refresh());
        }

        public void SelectShopPlane(int id) =>
            _currentShopPlane = GetPlane(id);

        public void Save(PlaneInfo planeInfo)
        {
            planeInfo.SaveSettings();
            if (planeInfo.ID == _currentPlane.ID)
                _currentPlane = planeInfo;
            if (planeInfo.ID == _currentShopPlane.ID)
                _currentShopPlane = planeInfo;
        }

        #endregion

        #region PRIVATE

        private PlaneInfo GetPlane(int id)
        {
            Planes.GetPlaneBy(id, out var plane);
            plane.LoadSettings();
            return plane;
        }

        #endregion
    }
}