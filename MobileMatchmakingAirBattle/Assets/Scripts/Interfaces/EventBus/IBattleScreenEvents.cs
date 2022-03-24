using TO;

namespace Interfaces.EventBus
{
    public interface IBattleScreenEvents : ISubscriber
    {
        void RefreshUI(AircraftDataModel dataModel);
        void RefreshHealthUI(AircraftDataModel dataModel);
    }
}