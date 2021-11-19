using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    public class AirCraftController : MonoBehaviour
    {
        [SerializeField] private byte _speed;

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
        }
    }
}