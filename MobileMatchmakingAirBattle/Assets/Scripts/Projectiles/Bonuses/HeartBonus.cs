using Managers.Gameplay;
using Photon.Pun;
using UnityEngine;

namespace Projectiles.Bonuses
{
    public class HeartBonus : MonoBehaviour
    {
        public BonusCreator BonusCreator { get; set; }
        public int _bonusHealth = 15;

        private void OnDestroy()
        {
            BonusCreator.RemoveBonus();
        }

        private void OnCollisionEnter(Collision other)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}