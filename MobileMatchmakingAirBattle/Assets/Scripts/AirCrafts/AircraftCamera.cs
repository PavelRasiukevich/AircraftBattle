using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {

        private void SetParent(Transform parent) => transform.SetParent(parent);

        private void SetPositionAndRotaionAccordingToParent(Transform slot) => transform.SetPositionAndRotation(slot.position, slot.rotation);

        private Transform FindSlotForCamera(Transform airCraft) => airCraft.GetComponentInChildren<CameraSlot>().transform;

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
    }
}