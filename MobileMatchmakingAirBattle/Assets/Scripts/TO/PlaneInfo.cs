using System;
using Assets.Scripts.AirCrafts;
using UnityEngine;
using Utils;

namespace TO
{
    [Serializable]
    public class PlaneInfo
    {
        [SerializeField] private AirCraft _planePrefab;

        public AirCraft PlanePrefab
        {
            get => _planePrefab;
            set => _planePrefab = value;
        }

        [SerializeField] private GameObject _planeShopModel;

        public GameObject PlaneShopModel
        {
            get => _planeShopModel;
            set => _planeShopModel = value;
        }

        [SerializeField] private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
        }

        [SerializeField] private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => _fileName = value;
        }

        [SerializeField] private AircraftSettings _settings;

        public AircraftSettings Settings
        {
            get => _settings;
            set => _settings = value;
        }

        #region PRIVATE

        public void LoadSettings()
        {
            if (Data.IsExists(FileName))
            {
                AircraftSettings settings = Data.Get<AircraftSettings>(FileName);
                _settings = settings;
            }
            else
            {
                AircraftSettings settings = new AircraftSettings();
                _settings = settings;
            }
        }

        public void SaveSettings()
        {
            Data.Set(FileName, _settings);
        }

        #endregion
    }
}