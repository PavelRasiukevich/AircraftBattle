using Assets.Scripts.Core;
using Data;
using UI.Screens.Shop.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens.Shop
{
    public class ShopScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Shop;

        [SerializeField] private LayoutGroup _content;
        [SerializeField] private ShopItemLine _shopItemLine;

        #region UNITY

        private void OnEnable() => RefreshUI();

        #endregion

        #region PRIVATE

        private void RefreshUI()
        {
            foreach (var componentsInChild in _content.GetComponentsInChildren<ShopItemLine>())
                Destroy(componentsInChild.gameObject);
            GameDataManager.Inst.PlanesData._planeList.ForEach(item=> Debug.Log(item._displayName));
        }

        #endregion

        #region OnClick

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

        #endregion
    }
}