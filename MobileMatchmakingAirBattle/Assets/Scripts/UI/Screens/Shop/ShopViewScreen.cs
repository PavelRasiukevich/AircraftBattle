using Assets.Scripts.Core;
using Core.Base;
using Enums;
using Managers.Data;
using TMPro;
using UnityEngine;

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

        #region UNITY

        private void OnEnable()
        {
            _shopViewModel.Load(GameDataManager.Inst.ShopPLane);
            _name.text = GameDataManager.Inst.ShopPLane.DisplayName;
            _hp.text = GameDataManager.Inst.ShopPLane.PlanePrefab.Data.Hp.ToString();
            _speed.text = GameDataManager.Inst.ShopPLane.PlanePrefab.Data.Speed.MoveSpeed.ToString();
            _mobility.text = GameDataManager.Inst.ShopPLane.PlanePrefab.Data.Speed.RotationSpeed.ToString();
        }

        #endregion

        #region OnClick

        public void ColorOnClick(Color color)
        {
            GameDataManager.Inst.ShopPLane.Settings.Color = color;
            _shopViewModel.Load(GameDataManager.Inst.ShopPLane);
        }

        public void WeaponOnClick(BulletType bulletType)
        {
            GameDataManager.Inst.ShopPLane.Settings.BulletType = bulletType;
        }

        public void GoOnClick()
        {
            GameDataManager.Inst.Save(GameDataManager.Inst.ShopPLane);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        public void Exit() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

        #endregion
    }
}