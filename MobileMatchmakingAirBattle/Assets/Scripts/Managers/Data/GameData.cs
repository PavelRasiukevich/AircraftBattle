﻿using Assets.Scripts.Utils;
using Core;
using Managers.Data.ScriptableObjects;
using TO;
using UnityEngine;

namespace Managers.Data
{
    public class GameData : BaseInstance<GameData>
    {

        [SerializeField] private PlanesDataScriptableObject _planes;
        public PlanesDataScriptableObject Planes { get => _planes; private set => _planes = value; }

        private PlaneInfo _currentPlane;
        public PlaneInfo CurrentPlane => _currentPlane;

        #region Unity

        protected override void Awake()
        {

            base.Awake();

           /* Planes =
                AssetDatabase.LoadAssetAtPath(Const.PlanesDataPath,
                    typeof(PlanesDataScriptableObject)) as PlanesDataScriptableObject;*/

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadCurrentPlane();
        }

        #endregion

        #region PRIVATE

        private void LoadCurrentPlane()
        {
            if (PlayerPrefs.HasKey(Const.CurrentPlaneIdKey))
            {
                if (!Planes.GetPlaneBy(PlayerPrefs.GetInt(Const.CurrentPlaneIdKey), out _currentPlane))
                    SelectPlane(Planes.PlaneList[0].ID);
            }

            else SelectPlane(Planes.PlaneList[0].ID);
        }

        #endregion

        #region PUBLIC

        public void SelectPlane(int id)
        {
            Planes.GetPlaneBy(id, out _currentPlane);
            ScreenEventHolder.Inst.RefreshShop();
            PlayerPrefs.SetInt(Const.CurrentPlaneIdKey, id);
            PlayerPrefs.Save();
        }

        #endregion
    }
}