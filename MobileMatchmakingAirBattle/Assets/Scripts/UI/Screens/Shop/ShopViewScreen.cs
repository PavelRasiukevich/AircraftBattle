using System.Globalization;
using Assets.Scripts.Core;
using Core.Base;
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

        private void OnEnable()
        {
            _shopViewModel.Load(GameDataManager.Inst.CurrentShopPlane);
            _name.text = GameDataManager.Inst.CurrentShopPlane.DisplayName;
            _hp.text = GameDataManager.Inst.CurrentShopPlane.PlanePrefab.Data.Hp.ToString();
            _speed.text = GameDataManager.Inst.CurrentShopPlane.PlanePrefab.Data.Speed.MoveSpeed.ToString();
            _mobility.text = GameDataManager.Inst.CurrentShopPlane.PlanePrefab.Data.Speed.RotationSpeed.ToString();
            _firePower.text = GameDataManager.Inst.CurrentShopPlane.FirePower.ToString(CultureInfo.InvariantCulture);
            _gunsCount.text = GameDataManager.Inst.CurrentShopPlane.GunsCount.ToString();
        }

        #endregion

        #region OnClick

        public void ColorOnClick(Color color)
        {
            GameDataManager.Inst.CurrentShopPlane.Settings.Color = color;
            GameDataManager.Inst.Save(GameDataManager.Inst.CurrentShopPlane);
            _shopViewModel.Load(GameDataManager.Inst.CurrentShopPlane);
        }


        public void GoOnClick()
        {
            GameDataManager.Inst.SelectPlane(GameDataManager.Inst.CurrentShopPlane.ID);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        #endregion
    }
}