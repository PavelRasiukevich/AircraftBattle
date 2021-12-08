using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {
        public void SetParent(Transform parent) => transform.SetParent(parent);

        public void SetPositionAndRotaion() => transform.SetPositionAndRotation(transform.parent.position, transform.parent.rotation);

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        public Transform FindSlotForCamera(Transform aircraft) => aircraft.GetComponentInChildren<CameraSlot>().transform;
    }
}