using Assets.Scripts.AirCrafts;
using Photon.Pun;
using UnityEngine;
using System;

namespace Assets.Scripts.Gameplay
{
    public class Spawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _playerPrefab;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private AircraftCamera _airCraftCamera;
        [SerializeField] private Transform _origin;

        private void Awake()
        {
            var actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            var point = _spawnPoint[actorNumber - 1];
            point = SetupPointInWorld(point);

           var actor = InitializeActor(point.position, point.rotation);

            CameraSetup(actor.transform);
        }

        #region PRIVATE METHODS

        private GameObject InitializeActor(Vector3 position, Quaternion rotation) => PhotonNetwork.Instantiate(_playerPrefab.name, position, rotation);

        private Transform SetupPointInWorld(Transform point)
        {
            var direction = GetDirectionToLook(_origin, point);
            var lookRotation = Quaternion.LookRotation(direction);
            lookRotation = SetupRotation(lookRotation);
            point.rotation = lookRotation;
            return point;
        }

        private void CameraSetup(Transform t)
        {
            _airCraftCamera.Activate();
            _airCraftCamera.SetParent(_airCraftCamera.FindSlotForCamera(t));
            _airCraftCamera.SetPositionAndRotaion();
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