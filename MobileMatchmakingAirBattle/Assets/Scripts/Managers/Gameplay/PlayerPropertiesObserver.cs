using Assets.Scripts.Utils;
using Core;
using ExitGames.Client.Photon;
using Interfaces.EventBus.PlayerProperties;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Gameplay
{
    /*
     * Изменился цвет, фраги, фейлы и тд
     */
    public class PlayerPropertiesObserver : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.TryGetValue(Const.Properties.MaterialColor, out var clr))
            {
                EventBus.InvokeEvent<IBodyParams>(x => x.OnColorChanged(targetPlayer.ActorNumber, (Vector3) clr));
            }

            if (changedProps.TryGetValue(Const.Properties.Fails, out var fails))
            {
                EventBus.InvokeEvent<IStatsUpdate>(x => x.OnFailsChanged(targetPlayer.ActorNumber, (int) fails));
            }

            if (changedProps.TryGetValue(Const.Properties.Frags, out var frags))
            {
                EventBus.InvokeEvent<IStatsUpdate>(x => x.OnFragsChanged(targetPlayer.ActorNumber, (int) frags));
            }
        }
    }
}