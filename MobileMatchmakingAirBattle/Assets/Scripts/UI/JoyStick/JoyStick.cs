using UnityEngine;

namespace Assets.Scripts.UI.JoyStick
{
    public class JoyStick : MonoBehaviour
    {
        [SerializeField] private OuterCircle _outerCircle;
        [SerializeField] private InnerCircle _innerCircle;
        [SerializeField] private Transform _center;

        [SerializeField] private Vector2 position;
        [SerializeField] private Vector2 offset;
        [SerializeField] Vector2 direction;

        private Camera _camera;

        public bool Touched { get; set; }

        public Vector2 ScreenPixelsPoint { get; set; }
        public Vector2 WorldPoints { get; set; }

        private void Awake()
        {
            _camera = GameObject.FindGameObjectWithTag("UICameraBattle").GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Touched = true;

                ScreenPixelsPoint = Input.mousePosition;
                WorldPoints = _camera.ScreenToWorldPoint(ScreenPixelsPoint);
            }
            else
            {
                Touched = false;
            }



            if (Touched)
            {
                offset = WorldPoints - (Vector2)_center.transform.position;
                direction = offset.normalized;

                _innerCircle.transform.position = new Vector3(_center.transform.position.x + direction.x, _center.transform.position.y + direction.y, _camera.nearClipPlane + 1.0f);

                print($"World Coordinates: {WorldPoints}");
                print($"Offset: {offset}");
                print($"Direction: {direction.normalized}");
                print($"InnerCirclePosition After : {_innerCircle.transform.position}");
            }
        }
    }
}