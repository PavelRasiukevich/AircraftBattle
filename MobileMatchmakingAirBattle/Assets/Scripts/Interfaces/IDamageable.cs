using Photon.Realtime;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int value, Player owner);

        public void AddHealth(int value);

        public void Die(bool isHit);
    }
}