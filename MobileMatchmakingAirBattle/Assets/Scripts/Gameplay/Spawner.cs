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

        private void Awake()
        {
            SpawnPlayerOnARandomPoint();
        }

        private void SpawnPlayerOnARandomPoint()
        {
            var randomIndex = GetRandomIndex(_spawnPoint);
            var randomPoint = GetPointByIndex(randomIndex);

            var p = PhotonNetwork.Instantiate(_playerPrefab.name,
                randomPoint.position,
                Quaternion.identity);

            CameraSetup(p.transform);
        }

        private void CameraSetup(Transform t)
        {
            _airCraftCamera.Activate();
            _airCraftCamera.SetParent(t);
            _airCraftCamera.ResetSettings();
            _airCraftCamera.SetupCameraSettings();
        }

        private int GetRandomIndex(Transform[] array) => UnityEngine.Random.Range(0, array.Length);

        private Transform GetPointByIndex(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException();

            if (_spawnPoint == null || _spawnPoint.Length == 0) throw new Exception();

            return _spawnPoint[index];
        }
    }
}