using Assets.Scripts.Core;
using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens.MainMenu
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Image _playerAvatar;

        #region PUBLIC

        public void Config()
        {
            _playerName.text = User.Name;
            _playerAvatar.sprite = User.Sprite;
        }

        #endregion

        #region OnClick

        public void PlayerIconOnClick()
        {
            PopupHolder.CurrentPopup(PopupType.StatisticPopup).Config().Show();
        }

        #endregion
    }
}