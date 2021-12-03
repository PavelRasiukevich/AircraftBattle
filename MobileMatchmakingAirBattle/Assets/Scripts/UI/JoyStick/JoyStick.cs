using UnityEngine;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private OuterCircle _outerCircle;
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private Transform _center;

        public static Vector3 VVN => InnerCircle.VelocityVectorNorm;

        public OuterCircle OuterCircle => _outerCircle;
        public InnerCircle InnerCircle => _innerCircle;
        public Transform Center => _center;

        private Camera _camera;

        private void Awake()
        {
            _camera = GameObject.FindGameObjectWithTag("UICameraBattle").GetComponent<Camera>();

            _innerCircle.Camera = _camera;
            _innerCircle.Origin = _center;

            _innerCircle.OuterCircleRadius = CalculateOuterCircleRadius(_outerCircle.transform);
            _innerCircle.InnerCircleRadius = CalculateOuterCircleRadius(_innerCircle.transform);

        }

        private float CalculateOuterCircleRadius(Transform circle)
        {
            var w = circle.GetComponent<RectTransform>().rect.width;
            return w / w;
        }
    }
}