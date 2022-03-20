using System;
using Enums;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class AircraftSettings
    {
        [SerializeField] private Color _color;
        [SerializeField] private BulletType _bulletType;

        public AircraftSettings()
        {
            _color = Color.red;
            _bulletType = BulletType.Default;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public BulletType BulletType
        {
            get => _bulletType;
            set => _bulletType = value;
        }
    }
}