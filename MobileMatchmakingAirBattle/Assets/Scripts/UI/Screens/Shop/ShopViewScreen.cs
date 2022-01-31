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
            _shopViewModel.Load(GameData.Inst.CurrentShopPlane);
            _name.text = GameData.Inst.CurrentShopPlane.DisplayName;
            _hp.text = GameData.Inst.CurrentShopPlane.PlanePrefab.Data.Hp.ToString();
            _speed.text = GameData.Inst.CurrentShopPlane.PlanePrefab.Data.Speed.MoveSpeed.ToString();
            _mobility.text = GameData.Inst.CurrentShopPlane.PlanePrefab.Data.Speed.RotationSpeed.ToString();
            _firePower.text = GameData.Inst.CurrentShopPlane.FirePower.ToString(CultureInfo.InvariantCulture);
            _gunsCount.text = GameData.Inst.CurrentShopPlane.GunsCount.ToString();
        }

        #endregion

        #region OnClick

        public void Color(Color color)
        {
            GameData.Inst.CurrentShopPlane.Settings.Color = color;
            GameData.Inst.ChangeInfoBy(GameData.Inst.CurrentShopPlane);
            _shopViewModel.Load(GameData.Inst.CurrentShopPlane);
        }


        public void GoOnClick()
        {
            GameData.Inst.SelectPlane(GameData.Inst.CurrentShopPlane.ID);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.Shop).ShowScreen();

        #endregion
    }
}