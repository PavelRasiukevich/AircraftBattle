using UnityEngine;
using Utils.Enums;

namespace UI.Screens.LeaderBoard.Elements
{
    public class LeaderboardTypePanel : MonoBehaviour
    {
        #region PUBLIC

        public void Config(LeaderboardType leaderboardType)
        {
            foreach (var btn in GetComponentsInChildren<LeaderboardItem>())
                btn.Config(leaderboardType);
        }

        #endregion
    }
}