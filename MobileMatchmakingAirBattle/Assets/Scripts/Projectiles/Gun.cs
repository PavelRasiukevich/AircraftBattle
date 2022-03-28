using Assets.Scripts.Audio;
using Photon.Realtime;
using TO;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _flareSpot;

        private Bullet _createdBullet;

        public void FireBullet(Bullet bullet, Player owner, AircraftDataModel DataModel)
        {

            _createdBullet = Instantiate(bullet, transform.position, transform.rotation);

            _createdBullet.AirCraftDataModel = DataModel;
            _createdBullet.Data.Owner = owner;

            Debug.Log(_createdBullet.Data.ScriptableData.Type.ToString());
            AudioController.Instance.PlaySound(_createdBullet.Data.ScriptableData.Type.ToString(), _createdBullet.gameObject);

            ExposeFlare(bullet);
        }

        private void ExposeFlare(Bullet bullet)
        {
            Instantiate(bullet.Data.ScriptableData.Flare, _flareSpot.position, _flareSpot.rotation);
        }
    }
}