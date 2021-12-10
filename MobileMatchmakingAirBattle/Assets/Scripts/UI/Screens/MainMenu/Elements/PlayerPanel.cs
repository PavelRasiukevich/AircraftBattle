using Assets.Scripts.Core;
using Network.External;
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
            Debug.Log(ExternalServices.Inst.PlayFab.ToString());
            if (!ExternalServices.Inst.PlayFab.Authenticate.IsReady)
                PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config("Need PlayFab!").Show();
            else
                PopupHolder.CurrentPopup(PopupType.StatisticPopup).Config().Show();
        }

        #endregion
    }
}