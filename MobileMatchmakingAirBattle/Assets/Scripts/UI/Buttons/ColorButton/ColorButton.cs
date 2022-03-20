using System;
using Managers.Data;
using UI.Screens.Shop;
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
        [SerializeField] private Color _planeColor;
        private SelectedEffect _selected;
        private ShopViewScreen _shopViewScreen;

        public Color Color
        {
            get => _planeColor;
            set => _planeColor = value;
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Click);
            _selected = gameObject.GetComponentInChildren<SelectedEffect>();
            _shopViewScreen = gameObject.GetComponentInParent<ShopViewScreen>();
        }

        private void Click() => _shopViewScreen.ColorOnClick(_planeColor);

        void Update()
        {
            if (_selected != null)
                _selected.gameObject.SetActive(
                    GameDataManager.Inst.CurrentShopPlane.Settings.Color.Equals(_planeColor));
        }
    }
}