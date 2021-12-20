using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {

        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private RectTransform _root;
        [SerializeField] private CanvasScaler _scaler;
        [SerializeField] private FireButton _fireButton;

        public static Vector2 JoystickInput { get;  set; }

        public static bool IsPressed { get; set; }

        public InnerCircle InnerCircle => _innerCircle;

        private void Awake()
        {
            _innerCircle.Radius = CalculateRadiusAccordingToResolution(_root);

        }

        private float CalculateRadiusAccordingToResolution(RectTransform transform)
        {
            var ratio = _scaler.referenceResolution.x / Screen.width;
            return transform.rect.width / 2 / ratio;
        }

        public static Vector2 GetJoysticInput()
        {
            return default;
        }
    }
}