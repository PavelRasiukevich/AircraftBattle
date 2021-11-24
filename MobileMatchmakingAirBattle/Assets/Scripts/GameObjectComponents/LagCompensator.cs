using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class LagCompensator : MonoBehaviour, IPunObservable
    {
        public Vector3 NetworkPosition { get; private set; }

        private Rigidbody _rigidbody;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            if (stream.IsWriting)
            {
                stream.SendNext(_rigidbody.position);
                stream.SendNext(_rigidbody.velocity);
            }
            else
            {
                NetworkPosition = (Vector3)stream.ReceiveNext();
                _rigidbody.velocity = (Vector3)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTimestamp));
                NetworkPosition += lag * _rigidbody.velocity;
            }
        }

        public void Init(Rigidbody rigidBody)
        {
            _rigidbody = rigidBody;
        }
    }
}