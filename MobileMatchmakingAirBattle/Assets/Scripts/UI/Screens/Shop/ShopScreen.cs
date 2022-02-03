using Assets.Scripts.Core;
using Core;
using Core.Base;
using Interfaces.Subscriber;
using Managers.Data;
using UI.Screens.Shop.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens.Shop
{
    public class ShopScreen : BaseScreen, IShopRefreshHandler
    {
        public override ScreenType Type => ScreenType.Shop;

        [SerializeField] private LayoutGroup _content;
        [SerializeField] private ShopItemLine _shopItemLine;

        #region UNITY

        private void OnEnable()
        {
            EventBus<ShopScreen>.Subscribe(this);
            Refresh();
        }

        private void OnDisable()
        {
            EventBus<ShopScreen>.Unsubscribe(this);
        }

        #endregion

        #region PRIVATE

        public void Refresh()
        {
            foreach (var componentsInChild in _content.GetComponentsInChildren<ShopItemLine>())
                Destroy(componentsInChild.gameObject);
            GameData.Inst.Planes.PlaneList.ForEach(line =>
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