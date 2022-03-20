using System;
using Assets.Scripts.AirCrafts;
using UnityEngine;
using Utils;

namespace TO
{
    [Serializable]
    public class PlaneInfo 
    {
        [SerializeField] private int _id;

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        [SerializeField] private Sprite _icon;

        public Sprite Icon
        {
            get => _icon;
            set => _icon = value;
        }

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

        [SerializeField] private bool _isViewInEditor;

        public bool IsViewInEditor
        {
            get => _isViewInEditor;
            set => _isViewInEditor = value;
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

        /*
         * "Огневая мощь" (для магазина)
         */

        [SerializeField] private float _firePower;

        public float FirePower
        {
            get => _firePower;
            set => _firePower = value;
        }

        /*
         * Количество орудий (для магазина)
         */

        [SerializeField] private int _gunsCount;

        public int GunsCount
        {
            get => _gunsCount;
            set => _gunsCount = value;
        }

        /*
         * Цена в игровой валюте (для магазина)
         */

        [SerializeField] private float _gamePrice;

        public float GamePrice
        {
            get => _gamePrice;
            set => _gamePrice = value;
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

        public void SaveSettings() // TODO: Хранить удаленно
        {
            Data.Set(FileName, _settings);
        }

        #endregion
    }
}