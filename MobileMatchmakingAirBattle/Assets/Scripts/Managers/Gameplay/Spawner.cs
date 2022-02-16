using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using Cinemachine;
using Managers.Data;
using Photon.Pun;
using UnityEngine;
using Utils.Extensions;

namespace Managers.Gameplay
{
    public class Spawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Transform _origin;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private Transform point;
        private GameObject _actor;

        #region UNITY

        void Awake()
        {
            var actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            point = _spawnPoint[actorNumber - 1];
            point = SetupPointInWorld(point);
        }

        #endregion

        #region PUBLIC

        public void Spawn() => InitializeActor(point.position, point.rotation);

        #endregion

        #region PRIVATE

        private void InitializeActor(Vector3 position, Quaternion rotation)
        {
            _actor = PhotonNetwork.Instantiate("Planes/" + GameDataManager.Inst.CurrentPlane.PlanePrefab.name, position,
                rotation);
            var airCraft = _actor.GetComponent<AirCraft>();
            PhotonNetwork.LocalPlayer.SetPropertyValue(Const.Properties.MaterialColor,
                GameDataManager.Inst.CurrentPlane.Settings.Color.ToVector3());
            PhotonNetwork.LocalPlayer.SetPropertyValue(Const.Properties.Fails, 0);
            PhotonNetwork.LocalPlayer.SetPropertyValue(Const.Properties.Frags, 0);
            airCraft.Data.RespawnPosition = point;
        }
        
        private Transform SetupPointInWorld(Transform point)
        {
            var direction = GetDirectionToLook(_origin, point);
            var lookRotation = Quaternion.LookRotation(direction);
            lookRotation = SetupRotation(lookRotation);
            point.rotation = lookRotation;
            return point;
        }

        private static Vector3 GetDirectionToLook(Transform a, Transform b) => (a.position - b.transform.position).normalized;

        private static Quaternion SetupRotation(Quaternion lookRotation)
        {
            lookRotation.x = 0;
            lookRotation.z = 0;
            return lookRotation;
        }

        #endregion
    }
}