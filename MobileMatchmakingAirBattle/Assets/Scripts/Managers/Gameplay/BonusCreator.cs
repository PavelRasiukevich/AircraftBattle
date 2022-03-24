using Photon.Pun;
using Projectiles.Bonuses;
using UnityEngine;

namespace Managers.Gameplay
{
    public class BonusCreator : MonoBehaviourPunCallbacks
    {
        private float _maxTime = 30;
        private float _time = 0;

        private int _lastPosNum = 0;
        private int _bonusCount = 0;
        private int _maxBonusCount = 2;
        [SerializeField] private Transform[] _spawnPoint;

        void FixedUpdate()
        {
            if (_bonusCount >= _maxBonusCount) return;
            _time += Time.fixedTime;
            if (_time >= _maxTime)
            {
                _time = 0;
                SpawnBonus(_spawnPoint[_lastPosNum]);
                _lastPosNum++;
                if (_lastPosNum >= _spawnPoint.Length) _lastPosNum = 0;
            }
        }

        private void SpawnBonus(Transform point)
        {
            _bonusCount++;
            if (!PhotonNetwork.IsMasterClient) return;
            GameObject bonus = PhotonNetwork.Instantiate("Bonuses/Heart", point.position, point.rotation);
            bonus.GetComponent<HeartBonus>().BonusCreator = this;
        }

        public void RemoveBonus()
        {
            _bonusCount--;
        }
    }
}