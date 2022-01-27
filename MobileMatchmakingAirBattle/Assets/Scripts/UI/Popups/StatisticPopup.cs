using Assets.Scripts.Core;
using Core.Base;
using TMPro;
using TO;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Popups
{
    public class StatisticPopup : BasePopup
    {
        public override PopupType Type => PopupType.StatisticPopup;

        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _fightsCount;
        [SerializeField] private TMP_Text _winsCount;
        [SerializeField] private TMP_Text _fragsCount;
        [SerializeField] private TMP_Text _failsCount;
        [SerializeField] private Image _playerAvatar;

        #region PUBLIC

        public override BasePopup Config()
        {
            _playerName.text = User.Common.Name;
            _fightsCount.text = User.Statistic.Fights.ToString();
            _winsCount.text = User.Statistic.Wins.ToString();
            _fragsCount.text = User.Statistic.Frags.ToString();
            _failsCount.text = User.Statistic.Fails.ToString();
            _playerAvatar.sprite = User.Common.Sprite;
            return this;
        }

        #endregion

        #region PRIVATE

        private void LeaderboardBy(LeaderboardType scoreType)
        {
            Hide();
            ScreenHolder
                .SetCurrentScreen(ScreenType.Leaderboard)
                .Config(scoreType)
                .ShowScreen();
        }

        #endregion

        #region OnClick

        public void ShareOnClick() => new NativeShare().SetText("Statistic").Share();
        public void CloseOnClick() => Hide();
        public void WinsOnClick() => LeaderboardBy(LeaderboardType.Wins);
        public void FailsOnClick() => LeaderboardBy(LeaderboardType.Fails);
        public void FightsOnClick() => LeaderboardBy(LeaderboardType.Fights);
        public void FragsOnClick() => LeaderboardBy(LeaderboardType.Frags);

        #endregion
    }
}