using UI.Panels;
using UnityEngine;

namespace Assets.Scripts.Core
{
    /*
     * Модальные окна и т.д.
     * TODO: refactoring
     */
    public class PanelHolder : MonoBehaviour
    {
        [SerializeField] private UnexpectedErrorPanel _unexpectedErrorPanel;

        #region PUBLIC

        public void UnexpectedError(string errorText)
        {
            _unexpectedErrorPanel.Config(errorText);
            _unexpectedErrorPanel.gameObject.SetActive(true);
        }

        #endregion
    }
}