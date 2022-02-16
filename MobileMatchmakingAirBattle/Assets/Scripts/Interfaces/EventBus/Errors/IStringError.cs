namespace Interfaces.EventBus
{
    public interface IStringError : ISubscriber
    {
        void Error(string err);
    }
}