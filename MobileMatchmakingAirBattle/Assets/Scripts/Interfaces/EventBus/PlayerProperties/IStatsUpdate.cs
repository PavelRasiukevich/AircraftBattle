namespace Interfaces.EventBus.PlayerProperties
{
    public interface IStatsUpdate : ISubscriber
    {
        void OnFailsChanged(int actorNumber, int fails);
        void OnFragsChanged(int actorNumber, int frags);
    }
}