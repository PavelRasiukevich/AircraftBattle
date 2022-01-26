namespace Interfaces.Subscriber
{
    public interface IStringErrorHandler : ISubscriber
    {
        void Error(string error);
    }
}