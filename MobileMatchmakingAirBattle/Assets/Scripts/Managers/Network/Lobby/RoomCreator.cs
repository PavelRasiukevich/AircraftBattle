using Assets.Scripts.Utils;
using Photon.Pun;
using Photon.Realtime;

namespace Assets.Scripts.Network.Lobby
{
    public class RoomCreator
    {
        public void CreateRoomWithCustomOptions(CustomRoomProperites _customPropeties)
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = _customPropeties.MaxPlayers,
                CustomRoomPropertiesForLobby = new string[2] { UtilsConst.LowerBound, UtilsConst.UpperBound },
                CustomRoomProperties = _customPropeties.PTSRange,
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