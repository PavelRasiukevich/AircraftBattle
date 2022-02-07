using Photon.Pun;

namespace Assets.Scripts.AirCrafts
{
    public class FollowCinemachineCamera : MonoBehaviourPunCallbacks
    {
        public override void OnEnable()
        {
            if (!GetComponentInParent<PhotonView>().IsMine)
                gameObject.SetActive(false);
        }
    }
}