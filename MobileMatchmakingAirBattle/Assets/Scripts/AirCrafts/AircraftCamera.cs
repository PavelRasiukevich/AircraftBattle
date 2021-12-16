using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {
        private AirCraft _airCraftToFollow;
        private Transform _cameraSlot;
        private Vector3 _positionOffset;

        private Vector3 pastFollowerPosition;
        private Vector3 pastTargetPosition;

        private Vector3 _lerpV;

        private void Start()
        {
            _airCraftToFollow = GameObject.FindObjectOfType<AirCraft>();
            _cameraSlot = FindSlotForCamera(_airCraftToFollow.transform);
            SetPositionAndRotaionAccordingToParent(_cameraSlot);

            _positionOffset = _airCraftToFollow.transform.position - transform.position;
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
            /*transform.rotation = Quaternion.Slerp(transform.rotation, _cameraSlot.rotation, 0.015f);
            transform.position = Vector3.Lerp(transform.position, _cameraSlot.position, .1f);*/

            transform.position = _airCraftToFollow.transform.position - _positionOffset;
        }

        //test method
        private Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
        {
            float t = Time.deltaTime * speed;
            Vector3 v = (targetPosition - pastTargetPosition) / t;
            Vector3 f = pastPosition - pastTargetPosition + v;
            return targetPosition - v + f * Mathf.Exp(-t);
        }

        private void SetParent(Transform parent) => transform.SetParent(parent);

        private void SetPositionAndRotaionAccordingToParent(Transform parent) => transform.SetPositionAndRotation(parent.position, parent.rotation);

        private Transform FindSlotForCamera(Transform airCraft) => airCraft.GetComponentInChildren<CameraSlot>().transform;

        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);
    }
}