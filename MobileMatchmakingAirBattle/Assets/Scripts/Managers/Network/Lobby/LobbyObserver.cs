using System;
using System.Collections.Generic;
using Assets.Scripts.PlayersSettings;
using Assets.Scripts.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Network.Lobby
{
    [Serializable]
    public class CustomRoomProperites
    {
        [SerializeField] private byte _maxPlayers;
        [SerializeField] private int _playerMMR;

        public byte MaxPlayers => _maxPlayers;

        public Hashtable PTSRange { get; set; }

        public int PlayerMMR
        {
            get => _playerMMR;

            set { _playerMMR = value; }
        }
    }

    public class LobbyObserver : MonoBehaviourPunCallbacks
    {
        #region EXPOSED IN INSPECTOR

        [SerializeField] private int _initialMMR;
        [SerializeField] private CustomRoomProperites _customRoomProperties;

        #endregion

        #region PRIVATE FIELDS

        private Dictionary<string, RoomInfo> _rooms;
        private PlayerSettings _settings;

        #endregion

        #region HELPERS

        private RoomListUpdater _roomListUpdater;
        private MatchMaker _matchMaker;
        private RoomCreator _roomCreator;

        #endregion

        #region UNITY

        private void Awake()
        {
            _roomListUpdater = new RoomListUpdater();
            _matchMaker = new MatchMaker();
            _roomCreator = new RoomCreator();
        }

        #endregion

        #region PUN CALLBACKS

        public override void OnJoinedLobby()
        {
            MessagesUtilities.JoinLobbyMessage();

            _rooms = new Dictionary<string, RoomInfo>();

            #region Potential Refactoring

            _settings = new PlayerSettings
            {
                //replace with PlayerPrefs.GetInt()
                MMR = _initialMMR,
            };

            _customRoomProperties.PTSRange = new Hashtable
            {
                {Const.LowerBound, _settings.MMR - Const.Difference},
                {Const.UpperBound, _settings.MMR + Const.Difference}
            };

            #endregion
        }

        public override void OnLeftLobby() => MessagesUtilities.LeftLobbyMessage();

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            MessagesUtilities.RoomListUpdateMessage();

            _rooms = _roomListUpdater.UpdateCachedRoomList(roomList, _rooms);

            if (_rooms != null && _rooms.Count > 0)
                _matchMaker.MatchPlayers(_rooms, _settings);
            else
                _roomCreator.CreateRoomWithCustomOptions(_customRoomProperties);
        }

        public override void OnJoinRandomFailed(short returnCode, string message) =>
            _roomCreator.CreateRoomWithCustomOptions(_customRoomProperties);

        public override void OnCreatedRoom()
        {
        }

        public override void OnCreateRoomFailed(short returnCode, string message) =>
            print($"{returnCode} / Message: {message}");

        #endregion
    }
}