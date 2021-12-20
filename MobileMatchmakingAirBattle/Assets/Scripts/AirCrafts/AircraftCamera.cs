using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {
        [Range(0.0f, 10.0f)]
        [SerializeField] private float _lerpT;

        [Range(0.0f, 10.0f)]
        [SerializeField] private float _slerpT;

        private AirCraft _airCraftToFollow;
        private Transform _cameraSlot;
        private Vector3 _positionOffset;

        private Quaternion _interpolatedCameraRotationValue;
        private Vector3 _interpolatedCameraPositionValue;

        private void Start()
        {
            _airCraftToFollow = GameObject.FindObjectOfType<AirCraft>();
            _cameraSlot = FindSlotForCamera(_airCraftToFollow.transform);

            SetPositionAndRotaionAccordingToParent(_cameraSlot);

            transform.position += Vector3.one;
        }

        private void Update()
        {
            _interpolatedCameraRotationValue = Quaternion.Lerp(transform.rotation, _cameraSlot.rotation, _slerpT);
            _interpolatedCameraPositionValue = Vector3.Lerp(transform.position, _cameraSlot.position, _lerpT);
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
            transform.position = _interpolatedCameraPositionValue;
            transform.rotation = _interpolatedCameraRotationValue;
        }

        //is not working properly
        private Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
        {
            float t = Time.deltaTime * speed;
            Vector3 v = (targetPosition - pastTargetPosition) / t;
            Vector3 f = pastPosition - pastTargetPosition + v;
            return targetPosition - v + f * Mathf.Exp(-t);
        }

        private void SetParent(Transform parent) => transform.SetParent(parent);

        private void SetPositionAndRotaionAccordingToParent(Transform slot) => transform.SetPositionAndRotation(slot.position, slot.rotation);

        private Transform FindSlotForCamera(Transform airCraft) => airCraft.GetComponentInChildren<CameraSlot>().transform;

        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);
    }
}