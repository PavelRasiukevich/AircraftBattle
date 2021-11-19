using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.AirCrafts
{
    [RequireComponent(typeof(UniversalAdditionalCameraData), typeof(Camera))]
    public class AircraftCamera : MonoBehaviour
    {
        private UniversalAdditionalCameraData _airCraftCamera;

        private void Awake()
        {
            _airCraftCamera = GetComponent<UniversalAdditionalCameraData>();
            var overlayCamera = GameObject.FindWithTag(UtilsConst.UICameraBattle).GetComponent<Camera>();
            _airCraftCamera.cameraStack.Add(overlayCamera);
        }

        public void SetParent(Transform parent) => transform.SetParent(parent);

        /// <summary>
        /// Resets position, rotation.
        /// </summary>
        public void ResetSettings() => transform.SetPositionAndRotation(transform.parent.position, transform.parent.rotation);

        public void SetupCameraSettings()
        {
            //setting to setup
        }

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
    }
}