using Utils.Enums;

namespace Assets.Scripts.Utils
{
    public class Const
    {
        public const string MMR = "mmr";

        public const string LowerBound = "low";
        public const string UpperBound = "up";

        public const string Battle = "Battle";

        public const int Difference = 125;
        public const int SearchWidth = 5;

        public const string UICameraBattle = "UICameraBattle";

        public const string PlanesDataPath = "Assets/PlanesData.asset";

        public const string CurrentPlaneIdKey = "CurrPlane";
        
        /*
         * PlayFab
         */
        internal class PlayFab
        {
            public const string TitleId = "36AB7";
            private const string SCORE_TOTALWINS = "TotalWinsScore";
            private const string SCORE_TOTALFRAGS = "TotalFragsScore";
            private const string SCORE_TOTALFAILS = "TotalFailScore";
            private const string SCORE_TOTALFIGHTS = "TotalFightsScore";

            public static string ScoreBy(LeaderboardType type)
            {
                string rez = SCORE_TOTALFIGHTS;
                switch (type)
                {
                    case LeaderboardType.Fights:
                        rez = SCORE_TOTALFIGHTS;
                        break;
                    case LeaderboardType.Wins:
                        rez = SCORE_TOTALWINS;
                        break;
                    case LeaderboardType.Frags:
                        rez = SCORE_TOTALFRAGS;
                        break;
                    case LeaderboardType.Fails:
                        rez = SCORE_TOTALFAILS;
                        break;
                }

                return rez;
            }
        }
    }
}