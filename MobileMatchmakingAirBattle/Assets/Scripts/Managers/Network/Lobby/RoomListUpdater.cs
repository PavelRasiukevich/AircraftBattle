using System.Collections.Generic;
using Photon.Realtime;

namespace Managers.Network.Lobby
{
    public class RoomListUpdater
    {
        public Dictionary<string, RoomInfo> UpdateCachedRoomList(
            List<RoomInfo> listOfRooms,
            Dictionary<string, RoomInfo> updatedListOfRooms
        )
        {
            for (int i = listOfRooms.Count - 1; i >= 0; i--)
            {
                RoomInfo info = listOfRooms[i];

                if (!info.RemovedFromList)
                    updatedListOfRooms.Add(info.Name, info);
                else
                    updatedListOfRooms.Remove(info.Name);
            }

            return updatedListOfRooms;
        }
    }
}