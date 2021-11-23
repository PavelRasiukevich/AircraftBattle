using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Network.Google;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Transform _playerInfoPanel;
        [SerializeField] private Transform _googlePlayButton;
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private TMP_Text _playerName;

        public void Config()
        {
            if (GooglePlayManager.IsLoad)
            {
                _playerInfoPanel.gameObject.SetActive(true);
                _googlePlayButton.gameObject.SetActive(false);
                _playerName.text = Social.localUser.userName;
                StartCoroutine(nameof(LocalImage));
            }
            else
            {
                _playerInfoPanel.gameObject.SetActive(false);
                _googlePlayButton.gameObject.SetActive(true);
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

        public void GoogleSignInOnClick()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.Load).ShowScreen();
        }
    }
}