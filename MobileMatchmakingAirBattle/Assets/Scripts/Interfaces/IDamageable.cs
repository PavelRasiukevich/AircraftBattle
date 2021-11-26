using Photon.Realtime;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int value, Player owner);
    }
}