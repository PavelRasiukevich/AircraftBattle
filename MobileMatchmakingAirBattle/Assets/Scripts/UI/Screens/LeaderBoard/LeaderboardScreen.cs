using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Utils;
using Network.External;
using PlayFab.ClientModels;
using TMPro;
using UI.Screens.LeaderBoard.Elements;
using UnityEngine;
using Utils.Enums;

namespace UI.Screens.LeaderBoard
{
    public class LeaderboardScreen : BaseScreen
    {
        public override ScreenType Type => ScreenType.Leaderboard;

        [SerializeField] private LeaderBoardLine _linePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TMP_Text _noRecordsText;
        [SerializeField] private LeaderboardTypePanel _leaderboardTypePanel;
        private LeaderboardType LeaderboardType { get; set; } = LeaderboardType.Fights;

        #region UNITY

        private void OnEnable()
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Show();
            _noRecordsText.gameObject.SetActive(false);
            foreach (var line in _content.GetComponentsInChildren<LeaderBoardLine>())
                Destroy(line.gameObject);
            ExternalServices.Inst.PlayFab.Leaderboards.RequestLeaderboard(
                UtilsConst.PlayFab.ScoreBy(LeaderboardType));
        }

        #endregion

        #region PUBLIC

        public override BaseScreen Config(LeaderboardType scoreType)
        {
            LeaderboardType = scoreType;
            _leaderboardTypePanel.Config(LeaderboardType);
            return this;
        }

        public void ViewLeaderboard(List<PlayerLeaderboardEntry> leaderboard)
        {
            PopupHolder.CurrentPopup(PopupType.Loading).Hide();
            _leaderboardTypePanel.Config(LeaderboardType);
            leaderboard.ForEach(
                line =>
                {
                    GameObject g = Instantiate(_linePrefab.gameObject, _content);
                    g.GetComponent<LeaderBoardLine>().Config(line, LeaderboardType);
                }
            );
            _noRecordsText.gameObject.SetActive(leaderboard.Count == 0);
        }

        #endregion

        #region OnClick

        public void SwitchToMain() => ScreenHolder.SetCurrentScreen(ScreenType.MainMenu).ShowScreen();

        #endregion
    }
}