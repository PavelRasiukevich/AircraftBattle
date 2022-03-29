using Core.Base;
using TO;
using UnityEngine;

namespace Managers.Data
{
    public class GameDataManager : BaseInstance<GameDataManager>
    {
        [SerializeField] private PlaneInfo _plane;
        public PlaneInfo CurrentPlane => _plane;

        private PlaneInfo _shopPLane;
        public PlaneInfo ShopPLane => _shopPLane;

        #region Unity

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _plane.LoadSettings();
            _shopPLane = _plane;
        }

        #endregion

        #region PUBLIC

        public void Save(PlaneInfo planeInfo)
        {
            planeInfo.SaveSettings();
            _plane = planeInfo;
            _shopPLane = planeInfo;
        }

        #endregion
    }
}