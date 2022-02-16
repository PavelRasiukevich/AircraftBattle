namespace Interfaces.EventBus
{
    public interface IDestroy : ISubscriber
    {
        void DestroyAircraft();
    }
}