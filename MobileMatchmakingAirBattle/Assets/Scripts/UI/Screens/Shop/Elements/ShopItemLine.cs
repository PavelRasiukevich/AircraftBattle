using System.Globalization;
using Assets.Scripts.Core;
using Enums;
using Managers.Data;
using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Shop.Elements
{
    public class ShopItemLine : MonoBehaviour
    {
        // Data
        private int _id;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _mobility;

        // UI
        [SerializeField] private Image _background;
        [SerializeField] private Sprite _backgroundLight;
        [SerializeField] private Sprite _backgroundDark;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _selectButton;

        #region UNITY

        private void Awake()
        {
            _selectButton.onClick.AddListener(SelectOnClick);
            _infoButton.onClick.AddListener(InfoOnClick);
        }

        #endregion

        #region PUBLIC

        public void Config(PlaneInfo planeInfo)
        {
            // Data
            _id = planeInfo.ID;
            _icon.sprite = planeInfo.Icon;
            _name.text = planeInfo.DisplayName;
            _hp.text = planeInfo.PlanePrefab.Data.Hp.ToString(CultureInfo.InvariantCulture);
            _speed.text = planeInfo.PlanePrefab.Data.Speed.MoveSpeed.ToString(CultureInfo.InvariantCulture);
            _mobility.text = planeInfo.PlanePrefab.Data.Speed.RotationSpeed.ToString(CultureInfo.InvariantCulture);
            // UI

            _selectButton.gameObject.SetActive(true);
            _infoButton.gameObject.SetActive(true);
            if (GameDataManager.Inst.CurrentPlane.ID == planeInfo.ID)
            {
                //Самолет выбран
                _buyButton.gameObject.SetActive(false);
                _background.sprite = _backgroundLight;
            }
            else
            {
                _buyButton.gameObject.SetActive(false);
                _background.sprite = _backgroundDark;
            }
        }

        #endregion

        #region OnClick

        private void SelectOnClick()
        {
            GameDataManager.Inst.SelectPlane(_id);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        private void InfoOnClick()
        {
            GameDataManager.Inst.SelectShopPlane(_id);
            ScreenHolder.SetCurrentScreen(ScreenType.ShopView).ShowScreen();
        }

        #endregion
    }
}