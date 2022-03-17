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
     * Регистрация игрока в PlayFab
     */
    public class RegistrationScreen : BaseScreen, IPlayfabError
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
            EventBus.AddListener<IPlayfabError>(this);
            OnChanged();
        }

        private void OnDisable() => EventBus.RemoveListener<IPlayfabError>(this);

        #endregion

        #region Events

        public void OnChanged()
        {
            _confirmButton.interactable = _nameInput.text.Length > 0 && _mailInput.text.Length > 0 &&
                                          _passwordInput.text.Length > 6;
        }

        public void Error(PlayFabError error)
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ErrorMessage;
        }

        #endregion

        #region OnClick

        public void ConfirmOnClick()
        {
            ExternalServices.Inst.PlayFab.Authenticate.RegisterWithPlayFab(_nameInput.text, _passwordInput.text,
                _mailInput.text);
        }

        public void BackOnClick()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.Login).ShowScreen();
        }

        #endregion
    }
}