using Assets.Scripts.Core;
using Core;
using Core.Base;
using Interfaces.Subscriber;
using Managers.External;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens
{
    public class LoadScreen : BaseScreen, IStringErrorHandler
    {
        [SerializeField] private TMP_Text _authErrorText;
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _shareScreenButton;

        public override ScreenType Type => ScreenType.Load;

        #region UNITY

        private void Start() => ExternalServices.Inst.Authentication();

        private void OnEnable() => EventBus<LoadScreen>.Subscribe(this);

        private void OnDisable() => EventBus<LoadScreen>.Unsubscribe(this);

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