using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class InnerCircle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region PRIVATE FIELDS
        [SerializeField] private float delta;
        [SerializeField] private float ZVelocity;
        #endregion

        #region PROPERTIES

        public float Radius { get; set; }
        public static Vector3 VelocityVectorNorm { get; set; }
        public Camera Camera { get; set; }
        public bool IsJoystickTouching { get; private set; }

        #endregion

        private void Start()
        {
            ZVelocity = Mathf.Clamp(ZVelocity, 0.0f, 1.0f);
        }

        private void Update()
        {
            delta = Vector3.Distance(transform.position, transform.parent.position) / Radius;
            VelocityVectorNorm = GetVelocityVector().normalized * delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsJoystickTouching = true;
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
            StartCoroutine(nameof(ReturnToOriginRoutine));
        }

        #region PRIVATE METHODS
        private Vector3 GetVelocityVector() => transform.position - transform.parent.position;
        #endregion

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