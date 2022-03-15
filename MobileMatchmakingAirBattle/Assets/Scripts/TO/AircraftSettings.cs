using Assets.Scripts.Projectiles;
using Assets.Scripts.Utils.Enums;
using System;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class AircraftSettings
    {
        [SerializeField] private Color _color;
        [SerializeField] private BulletType _type;

        public AircraftSettings()
        {
            _color = Color.red;
            _type = BulletType.Default;
        }

        public AircraftSettings(Color color)
        {
            _color = color;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public BulletType Type { get => _type; set => _type = value; }
    }
}