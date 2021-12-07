using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private OuterCircle _outerCircle;
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private Transform _origin;

        [SerializeField] private RectTransform _root;

        public static Vector3 VVN => InnerCircle.VelocityVectorNorm;

        public OuterCircle OuterCircle => _outerCircle;
        public InnerCircle InnerCircle => _innerCircle;
        public Transform Origin => _origin;

        private Camera _camera;

        private void Awake()
        {
            _camera = GameObject.FindGameObjectWithTag("UICameraBattle").GetComponent<Camera>();

            _innerCircle.Camera = _camera;
            _innerCircle.Origin = _origin;

            _innerCircle.Radius = CalculateOuterCircleRadius(_root);

        }

        private float CalculateOuterCircleRadius(RectTransform trsfrm)
        {
            var w = trsfrm.rect.width;
            print(w);
            return w / 2;
        }
    }
}