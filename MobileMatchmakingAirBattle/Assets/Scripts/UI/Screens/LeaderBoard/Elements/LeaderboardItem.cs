using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

namespace UI.Screens.LeaderBoard.Elements
{
    /*
     * TODO:  extend Button
     */
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] public Image _boardImage;
        [SerializeField] private LeaderboardType _leaderboardType;


        #region UNITY

        protected void Awake() => GetComponent<Button>().onClick.AddListener(LeaderboardTypeOnClick);

        #endregion

        #region PUBLIC

        public void Config(LeaderboardType scoreType) =>
            _boardImage.gameObject.SetActive(_leaderboardType == scoreType);

        #endregion

        #region OnClick

        void LeaderboardTypeOnClick() =>
            ScreenHolder.SetCurrentScreen(ScreenType.Leaderboard).Config(_leaderboardType).ShowScreen();

        #endregion
    }
}