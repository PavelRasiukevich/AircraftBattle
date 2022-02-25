using UnityEngine;

namespace Assets.Scripts
{
    public class FreeFall : MonoBehaviour
    {
        [SerializeField] private Transform _viwe;

        private Rigidbody _rb;

        private ControlsAsset _asset;

        private void Awake()
        {
            _asset = new ControlsAsset();

            var b = TryGetComponent<Rigidbody>(out var rb);
            _rb = b ? rb : throw new System.Exception("No rb component");

            _rb.useGravity = false;
        }

        private void OnEnable()
        {
            _asset.Enable();
        }

        private void OnDisable()
        {
            _asset.Disable();
        }

        private void Update()
        {
            if (_asset.Player.Moves.inProgress)
            {
                _rb.useGravity = false;
                _rb.velocity = Vector3.forward * 0.1f;
            }
            else
            {
                Fall();
            }
        }

        private void Fall()
        {

            _rb.useGravity = true;
            //V1();
            var rot = Quaternion.Euler(Vector3.right * 90);
            _viwe.rotation = Quaternion.Slerp(_viwe.rotation, rot, Time.deltaTime);

        }

        private void V1()
        {
            var angle = _rb.rotation.eulerAngles.x;

            angle = Mathf.Clamp(angle, 0, 90);

            _rb.rotation = Quaternion.Euler(angle, _rb.rotation.eulerAngles.y, _rb.rotation.eulerAngles.z);

            Quaternion fallRotation = Quaternion.Euler(Vector3.right);

            _rb.MoveRotation(_rb.rotation * fallRotation);
        }
    }
}