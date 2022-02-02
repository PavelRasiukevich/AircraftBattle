using System.Collections.Generic;
using Assets.Scripts.PlayersSettings;
using Assets.Scripts.Utils;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Network.Lobby
{
    public class MatchMaker
    {
        public void MatchPlayers(Dictionary<string, RoomInfo> rooms, PlayerSettings settings)
        {
            for (int t = Const.SearchWidth - 1; t >= 0; t--)
            {
                foreach (var value in rooms.Values)
                {
                    var room = value;

                    if (!CheckRoomConnectionConditions(room, settings)) continue;

                    settings.ResetChangedMMR();
                    PhotonNetwork.JoinRoom(room.Name);
                    Debug.Log("UserId  " + PhotonNetwork.LocalPlayer.UserId);
                    return;
                }

                settings.ExpandMMRBoundaries(Const.Difference);
            }

            //reset MMR to initial?

            JoinRandomRoom();
        }

        private bool CheckRoomConnectionConditions(RoomInfo room, PlayerSettings settings)
        {
            #region Cached

            var customProperties = room.CustomProperties;
            var cachedLowerBound = (int) room.CustomProperties[Const.LowerBound];
            var cachedUpperBound = (int) room.CustomProperties[Const.UpperBound];
            var increasedMMR = settings.IncreasedMMR;
            var decreasedMMR = settings.DecreasedMMR;

            #endregion

            if (customProperties == null || customProperties.Count == 0) return false;
            if (!customProperties.ContainsKey(Const.LowerBound)) return false;
            if (!customProperties.ContainsKey(Const.UpperBound)) return false;

            return increasedMMR >= cachedLowerBound
                   && increasedMMR <= cachedUpperBound
                   || decreasedMMR >= cachedLowerBound
                   && decreasedMMR <= cachedUpperBound;
        }

        private void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    }
}