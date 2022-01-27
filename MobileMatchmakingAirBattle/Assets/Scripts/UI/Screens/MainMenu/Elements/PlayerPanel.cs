using Assets.Scripts.Core;
using Core;
using Managers.External;
using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens.MainMenu.Elements
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private TMP_Text _goldText;

        #region PUBLIC

        public void Config()
        {
            _playerName.text = User.Common.Name;
            _playerAvatar.sprite = User.Common.Sprite;
            Debug.Log($"User.Currency.Count = {User.Currency.Count}");
            _goldText.text = User.Currency.Count.ToString();
        }

        #endregion

        #region OnClick

        public void PlayerIconOnClick()
        {
            Debug.Log(ExternalServices.Inst.PlayFab.ToString());
            if (!ExternalServices.Inst.PlayFab.Authenticate.IsReady)
                PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config("Need PlayFab!").Show();
            else
                PopupHolder.CurrentPopup(PopupType.StatisticPopup).Config().Show();
        }

        #endregion
    }
}