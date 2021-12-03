using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class InnerCircle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Transform Origin { get; set; }
        public float OuterCircleRadius { get; set; }
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
            delta = Vector3.Distance(transform.position, Origin.position) / OuterCircleRadius;
            VelocityVectorNorm = (transform.position - Origin.position).normalized * delta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var pixelScreenPosition = Input.mousePosition;
            var pixleToWorldPosition = Camera.ScreenToWorldPoint(pixelScreenPosition);

            if (IsJoystickTouching)
            {
                transform.position = pixleToWorldPosition;
                var zPos = transform.position;
                zPos.z = 100;
                transform.position = zPos;

                var distance = Vector3.Distance(Origin.position, transform.position);

                if (distance > OuterCircleRadius)
                {
                    var differenceBetweenCenterAndCirclePosition = transform.position - Origin.position;
                    differenceBetweenCenterAndCirclePosition *= OuterCircleRadius / distance;

                    transform.position = Origin.position + differenceBetweenCenterAndCirclePosition;
                }
                else
                {

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