using Assets.Scripts.AirCrafts;
using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Spawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _playerPrefab;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Transform _origin;

        private Transform point;
        private GameObject _actor;

        private void Awake()
        {
            var actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            point = _spawnPoint[actorNumber - 1];
            point = SetupPointInWorld(point);

            InitializeActor(point.position, point.rotation);
        }

        #region PRIVATE METHODS

        private void InitializeActor(Vector3 position, Quaternion rotation)
        {
            _actor = PhotonNetwork.Instantiate(_playerPrefab.name, position, rotation);
            var airCraft = _actor.GetComponent<AirCraft>();
            airCraft.DataModel.RespawnPosition = point;

            airCraft.DieAction += Die;
        }

        private void Die()
        {
            PhotonNetwork.Destroy(_actor);
            StartCoroutine(nameof(Wait));
        }

        private void Respawn()
        {
            InitializeActor(point.position, point.rotation);
        }

        private IEnumerator Wait()
        {
            Debug.Log("Wait");
            yield return new WaitForSeconds(3.0f);
            Respawn();
            StopCoroutine(nameof(Wait));
        }

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