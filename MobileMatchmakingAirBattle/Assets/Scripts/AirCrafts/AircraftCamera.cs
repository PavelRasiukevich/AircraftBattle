using Assets.Scripts.Utils;
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

        private void Start()
        {
            _airCraftToFollow = GameObject.FindObjectOfType<AirCraft>();
            _cameraSlot = FindSlotForCamera(_airCraftToFollow.transform);
            SetPositionAndRotaionAccordingToParent(_cameraSlot);

            _positionOffset = _airCraftToFollow.transform.position - transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _airCraftToFollow.transform.position - _positionOffset;
        }

        private void SetParent(Transform parent) => transform.SetParent(parent);

        private void SetPositionAndRotaionAccordingToParent(Transform parent) => transform.SetPositionAndRotation(parent.position, parent.rotation);

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        private Transform FindSlotForCamera(Transform airCraft) => airCraft.GetComponentInChildren<CameraSlot>().transform;
    }
}