using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : MonoBehaviour
    {

        [SerializeField] private int _speed;

        private void Awake()
        {
            Destroy(this.gameObject, 1.0f);
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
        }
    }
}