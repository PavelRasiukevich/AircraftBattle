using System;
using Assets.Scripts.AirCrafts;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class PlaneInfo
    {
        public int _id;

        public string _displayName;

        public bool _isDefaultPlane;

        public bool _isForMoney;

        public float _gamePrice;

        public Sprite _icon;

        public AirCraft _planePrefab;

        public bool _isViewInEditor;
    }
}