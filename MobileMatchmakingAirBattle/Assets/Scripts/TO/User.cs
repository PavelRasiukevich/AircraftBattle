using System.Collections.Generic;
using Assets.Scripts.Utils;
using Managers.External;
using UnityEngine;
using Utils.Enums;

namespace TO
{
    /*
     * Информация по игроку
     * (логин, аватар, рейтинги)
     */
    public class User
    {
        internal class Common
        {
            public static string Name { get; set; }
            public static Sprite Sprite { get; set; }
        }

        internal class Currency
        {
            public static int Count { get; private set; }
            public static void CountUpdate(int count) => Count = count;
            public static void Subtract(int value) =>
                ExternalServices.Inst.PlayFab.Currencies.Subtract(value);
            public static void Add(int value) => 
                ExternalServices.Inst.PlayFab.Currencies.Add(value);
        }

        internal class Statistic
        {
            public static Dictionary<string, int> Data { get; } = new Dictionary<string, int>
            {
                {Const.PlayFab.ScoreBy(LeaderboardType.Wins), 0},
                {Const.PlayFab.ScoreBy(LeaderboardType.Fails), 0},
                {Const.PlayFab.ScoreBy(LeaderboardType.Frags), 0},
                {Const.PlayFab.ScoreBy(LeaderboardType.Fights), 0}
            };

            public static int Fights
            {
                get => Data[Const.PlayFab.ScoreBy(LeaderboardType.Fights)];
                set
                {
                    Data[Const.PlayFab.ScoreBy(LeaderboardType.Fights)] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(Const.PlayFab.ScoreBy(LeaderboardType.Fights),
                        value);
                }
            }

            public static int Wins
            {
                get => Data[Const.PlayFab.ScoreBy(LeaderboardType.Wins)];
                set
                {
                    Data[Const.PlayFab.ScoreBy(LeaderboardType.Wins)] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(Const.PlayFab.ScoreBy(LeaderboardType.Wins),
                        value);
                    ExternalServices.Inst.GooglePlay.Achievements.Wins(value);
                }
            }

            public static int Fails
            {
                get => Data[Const.PlayFab.ScoreBy(LeaderboardType.Fails)];
                set
                {
                    Data[Const.PlayFab.ScoreBy(LeaderboardType.Fails)] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(Const.PlayFab.ScoreBy(LeaderboardType.Fails),
                        value);
                }
            }

            public static int Frags
            {
                get => Data[Const.PlayFab.ScoreBy(LeaderboardType.Frags)];
                set
                {
                    Data[Const.PlayFab.ScoreBy(LeaderboardType.Frags)] = value;
                    ExternalServices.Inst.PlayFab.Statistics.SubmitScore(Const.PlayFab.ScoreBy(LeaderboardType.Frags),
                        value);
                    if (ExternalServices.Inst.GooglePlay.Authenticate.IsReady)
                        ExternalServices.Inst.GooglePlay.Achievements.Frags(value);
                }
            }
        }
    }
}