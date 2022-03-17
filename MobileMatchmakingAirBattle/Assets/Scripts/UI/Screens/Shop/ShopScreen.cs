using Assets.Scripts.Core;
using Core;
using Core.Base;
using Enums;
using Interfaces.EventBus;
using Managers.Data;
using UI.Screens.Shop.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Shop
{
    public class ShopScreen : BaseScreen, IShopScreenEvents
    {
        public override ScreenType Type => ScreenType.Shop;

        [SerializeField] private LayoutGroup _content;
        [SerializeField] private ShopItemLine _shopItemLine;

        #region UNITY

        private void OnEnable()
        {
            EventBus.AddListener<IShopScreenEvents>(this);
            Refresh();
        }

        private void OnDisable()
        {
            EventBus.RemoveListener<IShopScreenEvents>(this);
        }

        #endregion

        #region PRIVATE

        public void Refresh()
        {
            foreach (var componentsInChild in _content.GetComponentsInChildren<ShopItemLine>())
                Destroy(componentsInChild.gameObject);
            GameDataManager.Inst.Planes.PlaneList.ForEach(line =>
            {
                Instantiate(_shopItemLine.gameObject, _content.transform)
                    .GetComponent<ShopItemLine>()
                    .Config(line);
            });
        }

        #endregion

        #region OnClick

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

        #endregion
    }
}