using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Assets.Scripts.PlayersSettings
{
    public class ExtendedPlayer : Player
    {
        public List<ExtendedPlayer> PLayers { get; set; }

        protected internal ExtendedPlayer(string nickName, int actorNumber, bool isLocal) : base(nickName, actorNumber, isLocal)
        {
        }

        protected internal ExtendedPlayer(string nickName, int actorNumber, bool isLocal, Hashtable playerProperties) : base(nickName, actorNumber, isLocal, playerProperties)
        {
        }
    }
}