using Assets.Scripts.Core;
using TMPro;
using UnityEngine;
using Utils.Enums;

namespace UI.Popups
{
    public class UnexpectedErrorPopup : BasePopup
    {
        public override PopupType Type => PopupType.UnexpectedError;

        [SerializeField] private TMP_Text _errorText;

        #region PUBLIC

        public override BasePopup Config(string text)
        {
            _errorText.text = text;
            return this;
        }

        #endregion

        #region OnClick

        public void ShareOnClick()
        {
            new NativeShare().SetText("Unexpected Error").Share();
        }

        public void CloseOnClick()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}