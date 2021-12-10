using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private RectTransform _root;
        [SerializeField] private CanvasScaler _scaler;

        private static bool v;

        public static Vector2 JoystickInput => InnerCircle.JoystickInput;
        public static bool IsPressed => v;

        public InnerCircle InnerCircle => _innerCircle;

        private void Awake()
        {
            _innerCircle.Radius = CalculateRadiusAccordingToResolution(_root);
        }

        private void Update()
        {
            v = GetValue();
        }

        private float CalculateRadiusAccordingToResolution(RectTransform transform)
        {
            var ratio = _scaler.referenceResolution.x / Screen.width;
            return transform.rect.width / 2 / ratio;
        }

        private bool GetValue()
        {
           return _innerCircle.IsJoystickTouching;
        }
    }
}