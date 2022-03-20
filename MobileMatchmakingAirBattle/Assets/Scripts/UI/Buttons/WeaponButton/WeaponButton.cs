using System;
using Enums;
using Managers.Data;
using UI.Screens.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Buttons.WeaponButton
{
    [Serializable]
    public class WeaponEvent : UnityEvent<BulletType>
    {
    }

    public class WeaponButton : Button
    {
        [SerializeField] private BulletType _bulletType;
        private SelectedEffect _selected;
        private ShopViewScreen _shopViewScreen;

        public BulletType Type
        {
            get => _bulletType;
            set => _bulletType = value;
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Click);
            _selected = gameObject.GetComponentInChildren<SelectedEffect>();
            _shopViewScreen = gameObject.GetComponentInParent<ShopViewScreen>();
        }

        private void Click() => _shopViewScreen.WeaponOnClick(_bulletType);

        void Update()
        {
            if (_selected != null)
                _selected.gameObject.SetActive(
                    GameDataManager.Inst.CurrentShopPlane.Settings.BulletType.Equals(_bulletType));
        }
    }
}