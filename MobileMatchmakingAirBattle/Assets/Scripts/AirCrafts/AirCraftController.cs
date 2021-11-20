using Assets.Scripts.Utils;
using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.AirCrafts
{
    public class AirCraftController : MonoBehaviour, IPunInstantiateMagicCallback
    {
        [SerializeField] private byte _speed;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _fireSpot;

        [SerializeField] private CustomPrefabPool _pool;

        private PhotonView _view;

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            //TODO
            //get color according
            //to actornumber
        }

        private void Awake()
        {
            _view = GetComponent<PhotonView>();

            _pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<CustomPrefabPool>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TestStuff();
            }

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

                    _view.RPC(nameof(Fire), RpcTarget.All);
                }
            }
        }

        private void TestStuff()
        {
            print(_view.ViewID);
            print(_view.OwnerActorNr);
            print(_view.ControllerActorNr);
            print(_view.CreatorActorNr);
            _view.TransferOwnership(2001);
        }

        private object[] GetInitData()
        {
            object[] data = new object[1];
            data[0] = PhotonNetwork.LocalPlayer.UserId;
            return data;
        }

        [PunRPC]
        private void Fire() => Instantiate(_bulletPrefab, _fireSpot.position, Quaternion.identity);
    }
}