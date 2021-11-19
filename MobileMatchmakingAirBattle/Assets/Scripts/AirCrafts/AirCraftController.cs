using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    public class AirCraftController : MonoBehaviour
    {
        [SerializeField] private byte _speed;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        private PhotonView _view;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
        }

        private void Update()
        {

            if (_view.IsMine)
            {
                if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
                {
                    this.transform.position += _speed * Input.GetAxis("Vertical") * Time.deltaTime * Vector3.forward;
                }

                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    this.transform.position += _speed * Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.right;
                }
            }

            if (_view.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    print("Attack button pressed");

                    Instantiate(_bulletPrefab, _fireSpot.position, Quaternion.identity);
                }
            }
        }
    }
}