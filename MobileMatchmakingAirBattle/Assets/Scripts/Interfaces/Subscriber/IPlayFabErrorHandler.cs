using PlayFab;

namespace Interfaces.Subscriber
{
    public interface IPlayFabErrorHandler : ISubscriber
    {
        void Error(PlayFabError error);
    }
}