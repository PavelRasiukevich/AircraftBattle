using Assets.Scripts.Core;
using Network.External;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens
{
    public class LoadScreen : BaseScreen
    {
        [SerializeField] private TMP_Text _authErrorText;
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _shareScreenButton;

        public override ScreenType Type => ScreenType.Load;

        #region UNITY

        private void Start() => ExternalServices.Instance.Authentication();

        #endregion

        #region Events

        public void GoogleAuthError(string errorText)
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            _shareScreenButton.gameObject.SetActive(true);
            _exitGameButton.gameObject.SetActive(true);
            _authErrorText.gameObject.SetActive(true);
            _authErrorText.text = errorText;
        }

        #endregion

        #region OnClick

        public void ExitOnClick()
        {
            Application.Quit();
        }

        public void ShareScreenOnClick()
        {
            new NativeShare().SetText("Error In Load Screen!").Share();
        }

        #endregion
    }
}