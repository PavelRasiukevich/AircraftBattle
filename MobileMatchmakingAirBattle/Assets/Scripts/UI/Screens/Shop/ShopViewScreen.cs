using System.Globalization;
using Assets.Scripts.Core;
using Managers.Data;
using TMPro;
using UnityEngine;
using Utils.Enums;

namespace UI.Screens.Shop
{
    public class ShopViewScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.ShopView;

        [SerializeField] private ShopViewModel _shopViewModel;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _mobility;
        [SerializeField] private TMP_Text _firePower;
        [SerializeField] private TMP_Text _gunsCount;
        #region UNITY

        private void OnEnable() => Refresh();

        #endregion

        #region PRIVATE

        private void Refresh()
        {
            _shopViewModel.Load(GameData.Inst.CurrentShopPlane.PlaneShopModel);
            _name.text = GameData.Inst.CurrentShopPlane.DisplayName;
            _hp.text = GameData.Inst.CurrentShopPlane.PlanePrefab.DataModel.Hp.ToString(CultureInfo.InvariantCulture);
            _speed.text = GameData.Inst.CurrentShopPlane.PlanePrefab.DataModel.Speed.MoveSpeed.ToString(CultureInfo.InvariantCulture);
            _mobility.text = GameData.Inst.CurrentShopPlane.PlanePrefab.DataModel.Speed.RotationSpeed.ToString(CultureInfo.InvariantCulture);
            _firePower.text = GameData.Inst.CurrentShopPlane.FirePower.ToString(CultureInfo.InvariantCulture);
            _gunsCount.text = GameData.Inst.CurrentShopPlane.GunsCount.ToString();
        } 

        #endregion

        #region OnClick

        public void SelectOnClick()
        {
            GameData.Inst.SelectPlane(GameData.Inst.CurrentShopPlane.ID);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        #endregion
    }
}