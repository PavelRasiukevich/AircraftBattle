using System;
using Assets.Scripts.AirCrafts;
using Managers.Data;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

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

            var p = PhotonNetwork.Instantiate(
                GameData.Inst.CurrentPlane.PlanePrefab.name,
                randomPoint.position,
                Quaternion.identity);

            CameraSetup(p.transform);
        }

        #region PRIVATE METHODS

        private void CameraSetup(Transform t)
        {
            _airCraftCamera.Activate();
            _airCraftCamera.SetParent(_airCraftCamera.FindSlotForCamera(t));
            _airCraftCamera.SetPositionAndRotaion();
        }

        private int GetRandomIndex(Transform[] array) => Random.Range(0, array.Length);

        private Transform GetPointByIndex(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException();

            if (_spawnPoint == null || _spawnPoint.Length == 0) throw new Exception();

            return _spawnPoint[index];
        }

        private object[] GetInitData()
        {
            return new object[1];
        }

        #endregion
    }
}