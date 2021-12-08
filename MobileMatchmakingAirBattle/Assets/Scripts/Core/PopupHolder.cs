using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Enums;

namespace Assets.Scripts.Core
{
    /*
     * Модальные окна и т.д.
     */
    public class PopupHolder : MonoBehaviour
    {
        private static Dictionary<PopupType, BasePopup> _dictionary;

        private BasePopup[] _children;

        private static BasePopup _currentPopup;

        private void Awake()
        {
            _dictionary = new Dictionary<PopupType, BasePopup>();
            _children = GetComponentsInChildren<BasePopup>(true);
            foreach (var popup in _children)
                _dictionary.Add(popup.Type, popup.GetComponent<BasePopup>());
        }

        public static BasePopup CurrentPopup(PopupType screenType)
        {
            if (_currentPopup)
                _currentPopup.Hide();

            _currentPopup = _dictionary[screenType];

            return _currentPopup;
        }
    }
}