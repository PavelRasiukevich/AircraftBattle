using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private OuterCircle _outerCircle;
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private RectTransform _root;
        [SerializeField] private CanvasScaler _scaler;

        public static Vector3 VVN => InnerCircle.VelocityVectorNorm;

        public OuterCircle OuterCircle => _outerCircle;
        public InnerCircle InnerCircle => _innerCircle;

        private void Awake()
        {
            _innerCircle.Radius = CalculateOuterCircleRadius(_root);
        }

        private float CalculateOuterCircleRadius(RectTransform trsfrm)
        {
            var ratio = _scaler.referenceResolution.x / Screen.width;
            return trsfrm.rect.width / 2 / ratio;
        }
    }
}