using System;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class PlaneSettings
    {
        [SerializeField] private Color _color;

        public Color Color
        {
            get => _color;
            set => _color = value;
        }
    }
}