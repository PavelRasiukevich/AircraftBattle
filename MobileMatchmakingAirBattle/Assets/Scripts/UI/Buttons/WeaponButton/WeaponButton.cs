using System;
using Enums;
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
        [SerializeField] private WeaponEvent _event = new WeaponEvent();

        [SerializeField] private BulletType _bulletType;

        public BulletType Type
        {
            get => _bulletType;
            set => _bulletType = value;
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Click);
        }

        private void Click() => _event.Invoke(_bulletType);
    }
}