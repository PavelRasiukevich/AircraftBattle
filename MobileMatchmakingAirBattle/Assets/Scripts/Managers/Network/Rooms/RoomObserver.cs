using Assets.Scripts.Utils;
using Photon.Pun;
using Photon.Realtime;
<<<<<<< HEAD
using System.Collections.Generic;
=======
using UnityEngine;
>>>>>>> origin/develop

namespace Managers.Network.Rooms
{
    public enum Clients
    {
        Single,
        Multiple
    }

    public class RoomObserver : MonoBehaviourPunCallbacks
    {
        private List<Player> _playersInRoom;
        private byte _maxPlayers;
        private PhotonView _photonView;

        public Clients clientType;

        #region UNITY

        private void Awake()
        {
            _photonView = photonView.GetComponent<PhotonView>();
        }

        #endregion

        #region PUN CALLBACKS

        public override void OnJoinedRoom()
        {
            MessagesUtilities.JoinRoomMessage();

            _playersInRoom = GetAllPlayersInCurrentRoom();
            _maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;

            if (clientType.Equals(Clients.Single))
            {
<<<<<<< HEAD
=======
                #region DELETE AFTER TEST

>>>>>>> origin/develop
                if (_playersInRoom.Count < _maxPlayers) return;

                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;

                CheckForExtraPlayers(_playersInRoom);

                if (!PhotonNetwork.IsMasterClient) return;

                PhotonNetwork.LoadLevel(Const.Battle);
<<<<<<< HEAD
=======

                #endregion
>>>>>>> origin/develop
            }
        }

        public override void OnLeftRoom()
        {
            MessagesUtilities.PlayerLeftRoomMessage(1);
            ClearListOfPlayers();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            MessagesUtilities.PlayerEnterRoomMessage();

            _playersInRoom.Add(newPlayer);

            if (_playersInRoom.Count < _maxPlayers) return;

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            CheckForExtraPlayers(_playersInRoom);

            if (!PhotonNetwork.IsMasterClient) return;

            PhotonNetwork.LoadLevel(Const.Battle);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            MessagesUtilities.PlayerLeftRoomMessage();

            if (_playersInRoom.Count <= 0 || _playersInRoom == null) return;

            _playersInRoom.Remove(otherPlayer);
        }

        #endregion

        #region PRIVATE METHODS

        private void ClearListOfPlayers() => _playersInRoom.Clear();

        private List<Player> GetAllPlayersInCurrentRoom()
        {
            #region Cached Values

            Player[] cachedPlayersArray = PhotonNetwork.PlayerList;

            #endregion

            var players = new List<Player>();

            foreach (var player in cachedPlayersArray)
                players.Add(player);

            return players;
        }

        private void CheckForExtraPlayers(List<Player> players)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            for (int i = 0; i < players.Count; i++)
                if (i > 1)
                    _photonView.RPC(nameof(KickPlayerFromRoom), players[i]);
        }

        [PunRPC]
        private void KickPlayerFromRoom() => PhotonNetwork.LeaveRoom();

        #endregion
    }
}