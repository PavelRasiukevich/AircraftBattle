using System.Collections.Generic;
using Assets.Scripts.Utils;
using Network.External;
using UnityEngine;

namespace TO
{
    /*
     * Информация по игроку
     */
    public class User
    {
        public static string Name { get; set; }
        public static Sprite Sprite { get; set; }

        internal class Statistic
        {
            public static Dictionary<string, int> Data { get; } = new Dictionary<string, int>
            {
                {UtilsConst.PlayFab.SCORE_TOTALWINS, 0},
                {UtilsConst.PlayFab.SCORE_TOTALFAILS, 0},
                {UtilsConst.PlayFab.SCORE_TOTALFRAGS, 0},
                {UtilsConst.PlayFab.SCORE_TOTALFIGHTS, 0}
            };

            public static int Fights
            {
                get => Data[UtilsConst.PlayFab.SCORE_TOTALFIGHTS];
                set
                {
                    Data[UtilsConst.PlayFab.SCORE_TOTALFIGHTS] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(UtilsConst.PlayFab.SCORE_TOTALFIGHTS,
                        value);
                }
            }

            public static int Wins
            {
                get => Data[UtilsConst.PlayFab.SCORE_TOTALWINS];
                set
                {
                    Data[UtilsConst.PlayFab.SCORE_TOTALWINS] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(UtilsConst.PlayFab.SCORE_TOTALWINS, value);
                    ExternalServices.Inst.GooglePlay.Achievements.Wins(value);
                }
            }

            public static int Fails
            {
                get => Data[UtilsConst.PlayFab.SCORE_TOTALFAILS];
                set
                {
                    Data[UtilsConst.PlayFab.SCORE_TOTALFAILS] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(UtilsConst.PlayFab.SCORE_TOTALFAILS, value);
                }
            }

            public static int Frags
            {
                get => Data[UtilsConst.PlayFab.SCORE_TOTALFRAGS];
                set
                {
                    Data[UtilsConst.PlayFab.SCORE_TOTALFRAGS] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(UtilsConst.PlayFab.SCORE_TOTALFRAGS, value);
                    if (ExternalServices.Inst.GooglePlay.Authenticate.IsReady)
                        ExternalServices.Inst.GooglePlay.Achievements.Frags(value);
                }
            }
        }
    }
}