using Assets.Scripts.Core;
using Core;
using Core.Base;
using Enums;
using Interfaces.EventBus;
using Managers.External;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    /*
     * Авторизация в PlayFab
     */
    public class LoginScreen : BaseScreen, IPlayfabError
    {
        public override ScreenType Type => ScreenType.Login;

        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_Text _errorText;

        #region UNITY

        private void OnEnable()
        {
            EventBus.AddListener<IPlayfabError>(this);
            _errorText.text = string.Empty;
            OnChanged();
        }

        private void OnDisable() => EventBus.RemoveListener<IPlayfabError>(this);

        #endregion

        #region Events

        public void OnChanged()
        {
            _confirmButton.interactable = _nameInput.text.Length > 0 && _passwordInput.text.Length > 0;
        }

        public void Error(PlayFabError error)
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ErrorMessage;
        }

        #endregion


        #region OnClick

        public void ConfirmOnClick()
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Show();
            ExternalServices.Inst.PlayFab.Authenticate.LoginWithPlayFab(_nameInput.text, _passwordInput.text);
        }

        public void RegistrationOnClick()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.Registration).ShowScreen();
        }

        public void CustomIdOnClick()
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Show();
            ExternalServices.Inst.PlayFab.Authenticate.AuthenticateWithCustomId();
        }

        #endregion
    }
}