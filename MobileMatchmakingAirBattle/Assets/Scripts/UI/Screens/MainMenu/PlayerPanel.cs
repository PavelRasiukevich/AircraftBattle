using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Image _playerAvatar;

        public void Config()
        {
            _playerName.text = Player.PlayerName;
            _playerAvatar.sprite = Player.PlayerSprite;
        }
    }
}