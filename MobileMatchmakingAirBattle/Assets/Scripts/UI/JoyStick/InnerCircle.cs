using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class InnerCircle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Transform Origin { get; set; }
        public float Radius { get; set; }

        public static Vector3 VelocityVectorNorm { get; set; }

        private float delta;

        public Camera Camera { get; set; }

        public bool IsJoystickTouching { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsJoystickTouching = true;
            StopCoroutine(nameof(ReturnToOriginRoutine));
        }

        private void Update()
        {
            if (!IsJoystickTouching)
            {
                delta = Vector3.Distance(transform.position, transform.parent.position) / Radius;
            }

            VelocityVectorNorm = (transform.position - transform.parent.position).normalized * delta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var pixelScreenPosition = Input.mousePosition;

            if (IsJoystickTouching)
            {
                transform.position = pixelScreenPosition;

                var distance = Vector3.Distance(transform.position, transform.parent.position);

                if (distance > Radius)
                {
                    var diff = transform.position - transform.parent.position;
                    diff = (diff * Radius) / distance;

                    transform.position = transform.parent.position + diff;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsJoystickTouching = false;
            StartCoroutine(nameof(ReturnToOriginRoutine));
        }

        private IEnumerator ReturnToOriginRoutine()
        {
            while (true)
            {
                transform.position = Vector3.Lerp(transform.position, transform.parent.position, Time.deltaTime * 10.0f);

                if (Vector3.Distance(transform.parent.position, transform.position) < 0.1f)
                {
                    transform.position = transform.parent.position;
                    StopCoroutine(nameof(ReturnToOriginRoutine));
                }
                yield return null;
            }
        }
    }
}