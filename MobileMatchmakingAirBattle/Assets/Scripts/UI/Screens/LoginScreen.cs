using Assets.Scripts.Core;
using Network.External;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens
{
    /*
     * Авторизация в PlayFab
     */
    public class LoginScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Login;

        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_Text _errorText;

        #region UNITY

        private void OnEnable()
        {
            _errorText.text = string.Empty;
            OnChanged();
        }

        #endregion

        #region PUBLIC

        #region Events

        public void OnChanged()
        {
            _confirmButton.interactable = _nameInput.text.Length > 0 && _passwordInput.text.Length > 0;
        }

        public void OnLoginError(string errorText)
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = errorText;
        }

        #endregion


        #region OnClick

        public void ConfirmOnClick()
        {
            ExternalServicesManager.Instance.PlayFabManager.LoginWithPlayFab(_nameInput.text, _passwordInput.text);
        }

        public void RegistrationOnClick()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.Registration).ShowScreen();
        }

        #endregion

        #endregion
    }
}