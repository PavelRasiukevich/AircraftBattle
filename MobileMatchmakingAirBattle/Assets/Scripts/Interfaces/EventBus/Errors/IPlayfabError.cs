using PlayFab;

namespace Interfaces.EventBus
{
    public interface IPlayfabError : ISubscriber
    {
        void Error(PlayFabError err);
    }
}