using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buttons.ColorButton
{
    [Serializable]
    public class ColorEvent : UnityEvent<Color>
    {
    }

    public class ColorButton : Button
    {
        [SerializeField] private ColorEvent _event = new ColorEvent();
        
        [SerializeField] private Color _color;

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Click);
        }

        private void Click() => _event.Invoke(_color);
    }
}