using UnityEngine;

namespace Projectiles.Bonuses
{
    public class HeartBonus : MonoBehaviour
    {
        [SerializeField] private int _bonusHealth = 20;

        public int BonusHealth => _bonusHealth;

        private void OnCollisionEnter(Collision other)
        {
            gameObject.GetComponentInParent<Bonus>().RemoveBonus();
        }
    }
}