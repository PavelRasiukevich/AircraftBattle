using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class LagCompensator : MonoBehaviour, IPunObservable
    {  
        public Vector3 NetworkPosition { get; private set; }

        public Rigidbody Rigidbody { get; set; }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            if (stream.IsWriting)
            {
                stream.SendNext(Rigidbody.position);
                stream.SendNext(Rigidbody.rotation);
                stream.SendNext(Rigidbody.velocity);
            }
            else
            {
                NetworkPosition = (Vector3)stream.ReceiveNext();
                Rigidbody.rotation = (Quaternion)stream.ReceiveNext();
                Rigidbody.velocity = (Vector3)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTimestamp));
                NetworkPosition += Rigidbody.velocity * lag;
            }
        }
    }
}