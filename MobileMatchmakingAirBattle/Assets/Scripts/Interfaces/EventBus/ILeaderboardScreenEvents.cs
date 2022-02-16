using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Interfaces.EventBus
{
    public interface ILeaderboardScreenEvents : ISubscriber
    {
        void Refresh(List<PlayerLeaderboardEntry> leaderboard);
    }
}