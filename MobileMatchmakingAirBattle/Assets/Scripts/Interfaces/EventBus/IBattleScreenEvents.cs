using TO;

namespace Interfaces.EventBus
{
    public interface IBattleScreenEvents : ISubscriber
    {
        void RefreshUI(AircraftDataModel dataModel);
        void DamageUI(AircraftDataModel dataModel);
    }
}