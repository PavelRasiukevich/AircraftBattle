using Assets.Scripts.Core;
using Network.External;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens
{
    /*
     * Регистрация игрока в PlayFab
     */
    public class RegistrationScreen : BaseScreen
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_InputField _mailInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_Text _errorText;
        public override ScreenType Type => ScreenType.Registration;

        #region UNITY

        private void OnEnable()
        {
            _errorText.text = string.Empty;
            OnChanged();
        }

        #endregion

        #region Events

        public void OnChanged()
        {
            _confirmButton.interactable = _nameInput.text.Length > 0 && _mailInput.text.Length > 0 &&
                                          _passwordInput.text.Length > 6;
        }

        public void OnRegistrationError(string errorText)
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = errorText;
        }

        #endregion

        #region OnClick

        public void ConfirmOnClick()
        {
            ExternalServices.Instance.PlayFabAuthenticate.RegisterWithPlayFab(_nameInput.text, _passwordInput.text,
                _mailInput.text);
        }

        public void BackOnClick()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen();
        }

        #endregion
    }
}