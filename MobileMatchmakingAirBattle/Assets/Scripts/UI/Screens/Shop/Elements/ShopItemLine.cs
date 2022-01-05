using System.Globalization;
using Assets.Scripts.Core;
using Managers.Data;
using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

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
        [SerializeField] private Image _isSelectedIcon;

        #region UNITY

        private void Awake()
        {
            //  _buyButton.onClick.AddListener(BuyOnClick);
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
            _hp.text = planeInfo.PlanePrefab.DataModel.Hp.ToString(CultureInfo.InvariantCulture);
            _speed.text = planeInfo.PlanePrefab.DataModel.Speed.MoveSpeed.ToString(CultureInfo.InvariantCulture);
            _mobility.text = planeInfo.PlanePrefab.DataModel.Speed.RotationSpeed.ToString(CultureInfo.InvariantCulture);
            // UI
            if (GameData.Inst.CurrentPlane.ID == planeInfo.ID)
            {
                //Самолет выбран
                _isSelectedIcon.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
                _infoButton.gameObject.SetActive(false);
                _buyButton.gameObject.SetActive(false);
                _background.sprite = _backgroundLight;
            }
            else
            {
                _isSelectedIcon.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                _infoButton.gameObject.SetActive(true);
                _buyButton.gameObject.SetActive(false);
                _background.sprite = _backgroundDark;
            }
        }

        #endregion

        #region OnClick

        private void SelectOnClick()
        {
            GameData.Inst.SelectPlane(_id);
            ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();
        }

        private void InfoOnClick()
        {
            GameData.Inst.SelectShopPlane(_id);
            ScreenHolder.SetCurrentScreen(ScreenType.ShopView).ShowScreen();
        }

        #endregion
    }
}