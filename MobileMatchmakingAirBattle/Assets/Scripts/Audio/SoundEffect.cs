using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class SoundEffect : MonoBehaviour
    {
        private void Awake()
        {
            AudioController.Instance.PlaySound("Explosion", gameObject);
        }
    }
}