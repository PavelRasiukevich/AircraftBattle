using Photon.Realtime;
using TO;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _flareSpot;

        public void FireBullet(Bullet bullet, Player owner, AircraftDataModel DataModel)
        {
            var b = Instantiate(bullet, transform.position, transform.rotation);

            b.AirCraftDataModel = DataModel;
            b.Data.Owner = owner;

            ExposeFlare(bullet);
        }

        private void ExposeFlare(Bullet bullet)
        {
            Instantiate(bullet.Data.ScriptableData.Flare, _flareSpot.position, _flareSpot.rotation);
        }
    }
}