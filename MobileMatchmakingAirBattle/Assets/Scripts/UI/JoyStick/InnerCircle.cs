using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class InnerCircle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Transform Origin { get; set; }
        public float Radius { get; set; }
        public float InnerCircleRadius { get; set; }

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
            delta = Vector3.Distance(transform.position, Origin.position) / Radius;
            VelocityVectorNorm = (transform.position - Origin.position).normalized * delta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var pixelScreenPosition = Input.mousePosition;

            print($"Inner Circle Position IN PIXELS: {pixelScreenPosition}");


            if (IsJoystickTouching)
            {
                transform.position = pixelScreenPosition;

              //  print($"Inner Circle Position: {transform.position}");
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
                transform.position = Vector3.Lerp(transform.position, Origin.position, Time.deltaTime * 10.0f);

                if (Vector3.Distance(Origin.position, transform.position) < 0.1f)
                {
                    transform.position = Origin.position;
                    StopCoroutine(nameof(ReturnToOriginRoutine));
                }
                yield return null;
            }
        }
    }
}