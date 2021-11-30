using TMPro;
using UnityEngine;

namespace UI.Panels
{
    public class UnexpectedErrorPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _errorText;

        public void Config(string errorText)
        {
            _errorText.text = errorText;
        }

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