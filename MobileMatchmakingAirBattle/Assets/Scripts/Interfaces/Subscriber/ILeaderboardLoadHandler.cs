using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Interfaces.Subscriber
{
    public interface ILeaderboardLoadHandler : ISubscriber
    {
        void Refresh(List<PlayerLeaderboardEntry> leaderboard);
    }
}