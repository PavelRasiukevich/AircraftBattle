using System.Collections.Generic;
using Core.Base;
using UnityEngine;
using Utils.Enums;

namespace Assets.Scripts.Core
{
    public class ScreenHolder : MonoBehaviour
    {
        [SerializeField] private ScreenType _default;

        private static Dictionary<ScreenType, BaseScreen> _dictionary;
        private static BaseScreen _currentScreen;

        private BaseScreen[] _children;

        private void Awake()
        {
            _dictionary = new Dictionary<ScreenType, BaseScreen>();
            _children = GetComponentsInChildren<BaseScreen>(true);
            
            foreach (var screen in _children)
                _dictionary.Add(screen.Type, screen);
            _currentScreen = _dictionary[_default];
            
            _currentScreen.ShowScreen();
        }

        public static BaseScreen SetCurrentScreen(ScreenType screenType)
        {
            var nextScreen = _dictionary[screenType];

            if (_currentScreen)
                _currentScreen.HideScreen();

            _currentScreen = nextScreen;

            return _currentScreen;
        }
    }
}