using Assets.Scripts.Utils;
using Photon.Pun;
using Photon.Realtime;

namespace Managers.Network.Lobby
{
    public class RoomCreator
    {
        public void CreateRoomWithCustomOptions(CustomRoomProperites customPropeties)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = customPropeties.MaxPlayers,
                CustomRoomPropertiesForLobby = new[] { Const.LowerBound, Const.UpperBound },
                CustomRoomProperties = customPropeties.PTSRange,
                IsOpen = true,
                IsVisible = true,
                PlayerTtl = 12000,
                EmptyRoomTtl = 5000,
                // CleanupCacheOnLeave = false
            };

            PhotonNetwork.CreateRoom(null, roomOptions);
        }
    }
}