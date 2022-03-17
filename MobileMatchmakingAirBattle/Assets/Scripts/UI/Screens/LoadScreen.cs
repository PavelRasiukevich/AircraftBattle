using Core;
using Core.Base;
using Enums;
using Interfaces.EventBus;
using Managers.External;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LoadScreen : BaseScreen, IStringError
    {
        [SerializeField] private TMP_Text _authErrorText;
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _shareScreenButton;

        public override ScreenType Type => ScreenType.Load;

        #region UNITY

        private void Start() => ExternalServices.Inst.Authentication();

        private void OnEnable() => EventBus.AddListener<IStringError>(this);

        private void OnDisable() => EventBus.RemoveListener<IStringError>(this);

        #endregion

        #region Events

        public void Error(string error)
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            _shareScreenButton.gameObject.SetActive(true);
            _exitGameButton.gameObject.SetActive(true);
            _authErrorText.gameObject.SetActive(true);
            _authErrorText.text = error;
        }

        #endregion

        #region OnClick

        public void ExitOnClick() => Application.Quit();

        public void ShareScreenOnClick() => new NativeShare().SetText("Error In Load Screen!").Share();

        #endregion
    }
}