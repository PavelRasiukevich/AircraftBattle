using System;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class AircraftSettings
    {
        [SerializeField] private Color _color;

        public AircraftSettings()
        {
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
    }
}