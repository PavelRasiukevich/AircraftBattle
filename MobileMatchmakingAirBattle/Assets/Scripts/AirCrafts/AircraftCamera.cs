using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {

        private void Start()
        {
            var airCraft = GameObject.FindObjectOfType<AirCraft>();

            SetParent(FindSlotForCamera(airCraft.transform));
            SetPositionAndRotaionAccordingToParent(transform.parent);
        }

        private void SetParent(Transform parent) => transform.SetParent(parent);

        private void SetPositionAndRotaionAccordingToParent(Transform parent) => transform.SetPositionAndRotation(parent.position, parent.rotation);

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        private Transform FindSlotForCamera(Transform airCraft) => airCraft.GetComponentInChildren<CameraSlot>().transform;
    }
}