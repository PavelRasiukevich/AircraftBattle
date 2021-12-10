using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private RectTransform _root;
        [SerializeField] private CanvasScaler _scaler;

        public static Vector3 VelocityVector => InnerCircle.VelocityVectorNorm;

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
    }
}