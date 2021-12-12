using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class InnerCircle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region PRIVATE FIELDS
        private float delta;
        #endregion

        #region PROPERTIES

        public float Radius { get; set; }
        public bool IsJoystickTouching { get; private set; }
        public Vector2 VelocityVectorInput { get; private set; }

        #endregion

        private void Update()
        {
            delta = Vector3.Distance(transform.position, transform.parent.position) / Radius;
            JoyStick.JoystickInput = (transform.position - transform.parent.position).normalized * delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsJoystickTouching = true;
            JoyStick.IsPressed = IsJoystickTouching;
            StopCoroutine(nameof(ReturnToOriginRoutine));
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
            JoyStick.IsPressed = IsJoystickTouching;
            StartCoroutine(nameof(ReturnToOriginRoutine));
        }

        #region ROUTINES

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

        #endregion
    }
}