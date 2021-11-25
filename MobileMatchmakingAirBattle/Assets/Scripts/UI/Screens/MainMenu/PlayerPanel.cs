using System.Collections;
using Network.Google;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private TMP_Text _playerName;

        public void Config()
        {
            if (GooglePlayManager.IsLoad)
            {
                _playerName.text = Social.localUser.userName;
                StartCoroutine(nameof(LocalImage));
            }
        }

        private IEnumerator LocalImage()
        {
            while (Social.localUser.image == null)
                yield return null;
            _playerAvatar.sprite = Sprite.Create(Social.localUser.image,
                new Rect(0.0f, 0.0f, Social.localUser.image.width, Social.localUser.image.height),
                Vector2.zero);
        }
    }
}