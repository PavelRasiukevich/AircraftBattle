using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : MonoBehaviour, IPunInstantiateMagicCallback
    {

        [SerializeField] private int _speed;

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            print($"{info.Sender.ActorNumber}");
            info.Sender.TagObject = this.gameObject;
            var data = info.photonView.InstantiationData;
            print(data[0]);
        }

        private void Awake() => Destroy(this.gameObject, 5.0f);

        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TestStuff();
            }

        transform.position += _speed * Time.deltaTime * transform.forward;
        }

        private void TestStuff()
        {
            
        }
    }
}