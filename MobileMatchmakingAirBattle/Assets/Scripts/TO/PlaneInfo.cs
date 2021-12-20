using System;
using Assets.Scripts.AirCrafts;
using UnityEngine;

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

        [SerializeField] private  bool _isViewInEditor;

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


        /*
         * "Огневая мощь" визуальная настройка для магазина
         */

        [SerializeField] private  float _firePower;
        
        public float FirePower
        {
            get => _firePower;
            set => _firePower = value;
        }

        /*
         * Цена в игровой валюте
         */

        [SerializeField] private  float _gamePrice;
        
        public float GamePrice
        {
            get => _gamePrice;
            set => _gamePrice = value;
        }
    }
}