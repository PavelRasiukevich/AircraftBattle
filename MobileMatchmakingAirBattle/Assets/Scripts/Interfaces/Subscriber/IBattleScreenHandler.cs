using TO;

namespace Interfaces.Subscriber
{
    public interface IBattleScreenHandler : ISubscriber
    {
        void RefreshUI(AircraftDataModel dataModel);
        void DamageUI(AircraftDataModel dataModel);
        void SwitchWeaponUI(AircraftDataModel dataModel);
    }
}