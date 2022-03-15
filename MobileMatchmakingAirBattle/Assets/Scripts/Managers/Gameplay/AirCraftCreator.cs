using Assets.Scripts.AirCrafts;
using Assets.Scripts.GameObjectComponents;
using Managers.Data;
using Photon.Pun;
using UnityEngine;

namespace Managers.Gameplay
{
    public class AirCraftCreator : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Transform _origin;

        private Transform point;
        private GameObject _actor;

        private BattleManager _battleManager;

        #region UNITY

        void Awake()
        {
            var actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            point = _spawnPoint[actorNumber - 1];
            point = SetupPointInWorld(point);
            _battleManager = GetComponent<BattleManager>();
        }

        #endregion

        #region PUBLIC

        public void Create() => InitializeActor(point.position, point.rotation);

        #endregion

        #region PRIVATE

        private void InitializeActor(Vector3 position, Quaternion rotation)
        {
            _actor = PhotonNetwork.Instantiate("Planes/" + GameDataManager.Inst.CurrentPlane.PlanePrefab.name, position,
                rotation);
            var airCraft = _actor.GetComponent<AirCraft>();
            var t = _actor.GetComponent<InteractionsHandler>();

            airCraft.Data.RespawnPosition = point;

            t.Died += _battleManager.GameFail;
        }

        private Transform SetupPointInWorld(Transform point)
        {
            var direction = GetDirectionToLook(_origin, point);
            var lookRotation = Quaternion.LookRotation(direction);
            lookRotation = SetupRotation(lookRotation);
            point.rotation = lookRotation;
            return point;
        }

        private static Vector3 GetDirectionToLook(Transform a, Transform b) =>
            (a.position - b.transform.position).normalized;

        private static Quaternion SetupRotation(Quaternion lookRotation)
        {
            lookRotation.x = 0;
            lookRotation.z = 0;
            return lookRotation;
        }

        #endregion
    }
}