using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Enums;

namespace UI.Screens.LeaderBoard.Elements
{
    /*
     * TODO: refactoring обсудить
     */
    public class LeaderBoardLine : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionNum;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _score;

        [SerializeField] private Image[] _goldSilverBronzeIcons;
        [SerializeField] private Image _LeaderBoardIcon;

        #region PUBLIC

        public void Config(PlayerLeaderboardEntry line, LeaderboardType leaderboardType)
        {
            if (line.Position < _goldSilverBronzeIcons.Length)
            {
                _positionNum.gameObject.SetActive(false);
                _goldSilverBronzeIcons[line.Position].gameObject.SetActive(true);
            }

            _positionNum.text = (++line.Position).ToString();
            _playerName.text = line.DisplayName;
            _score.text = line.StatValue.ToString();

            switch (leaderboardType)
            {
                case LeaderboardType.Fails:
                    _LeaderBoardIcon.sprite = ResourcesReader.FailsSprite;
                    break;
                case LeaderboardType.Fights:
                    _LeaderBoardIcon.sprite = ResourcesReader.FightsSprite;
                    break;
                case LeaderboardType.Frags:
                    _LeaderBoardIcon.sprite = ResourcesReader.FragsSprite;
                    break;
                case LeaderboardType.Wins:
                    _LeaderBoardIcon.sprite = ResourcesReader.WinsSprite;
                    break;
            }
        }

        #endregion
    }
}