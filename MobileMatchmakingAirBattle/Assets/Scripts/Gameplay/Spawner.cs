using Assets.Scripts.AirCrafts;
using Cinemachine;
using Managers.Data;
using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Spawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Transform _origin;
        [SerializeField] private CinemachineVirtualCamera _camera;

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
            _actor = PhotonNetwork.Instantiate("Planes/"+GameData.Inst.CurrentPlane.PlanePrefab.name, position, rotation);

         /*   if (_actor.GetComponent<AirCraft>().photonView.IsMine)
            {
                _camera.Follow = _actor.GetComponentInChildren<CameraSlot>().transform;
                _camera.LookAt = _actor.transform;
            }*/

            var airCraft = _actor.GetComponent<AirCraft>();
            airCraft.Data.RespawnPosition = point;

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