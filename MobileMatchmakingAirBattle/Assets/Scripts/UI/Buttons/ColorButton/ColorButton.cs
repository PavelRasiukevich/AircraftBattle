using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Buttons.ColorButton
{
    [Serializable]
    public class ColorEvent : UnityEvent<Color>
    {
    }

    public class ColorButton : Button
    {
        [SerializeField] private ColorEvent _event = new ColorEvent();

        [SerializeField] private Color _planeColor;

        public Color Color
        {
            get => _planeColor;
            set => _planeColor = value;
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Click);
        }

        private void Click() => _event.Invoke(_planeColor);
    }
}