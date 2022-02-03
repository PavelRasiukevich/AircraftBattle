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

        public const string AircraftMaterialName = "RedCC0813";

        /*
         * Currencies
         */
        internal class Currencies
        {
            public const string Gold = "GD";
        }

        /*
         * Paths
         */
        internal class Path
        {
            public const string PlanesData = "Assets/ScriptableObjects/PlanesData.asset";
            public const string DefaultAvatar = "Sprite/Avatar/DefaultAvatar";
        }

        /*
         * Tags
         */
        internal class Tags
        {
            public const string UICameraBattle = "UICameraBattle";
            public const string FightArena = "FightArena";
            public const string Ground = "Ground";
        }

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