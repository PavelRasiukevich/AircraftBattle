using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Spawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _playerPrefab;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Transform _origin;

        private void Awake()
        {
            var actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            var point = _spawnPoint[actorNumber - 1];
            point = SetupPointInWorld(point);

           InitializeActor(point.position, point.rotation);

        }

        #region PRIVATE METHODS

        private void InitializeActor(Vector3 position, Quaternion rotation) => PhotonNetwork.Instantiate(_playerPrefab.name, position, rotation);

        private Transform SetupPointInWorld(Transform point)
        {
            var direction = GetDirectionToLook(_origin, point);
            var lookRotation = Quaternion.LookRotation(direction);
            lookRotation = SetupRotation(lookRotation);
            point.rotation = lookRotation;
            return point;
        }

        private Vector3 GetDirectionToLook(Transform a, Transform b) => (a.position - b.transform.position).normalized;

        private static Quaternion SetupRotation(Quaternion lookRotation)
        {
            lookRotation.x = 0;
            lookRotation.z = 0;
            return lookRotation;
        }

        #endregion
    }
}