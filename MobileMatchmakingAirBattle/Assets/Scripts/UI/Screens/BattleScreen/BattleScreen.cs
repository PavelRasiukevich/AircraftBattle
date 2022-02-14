using Core;
using Core.Base;
using Interfaces.Subscriber;
using TO;
using UI.Screens.BattleScreen.Elements;
using Utils.Enums;

namespace UI.Screens.BattleScreen
{
    public class BattleScreen : BaseScreen, IBattleScreenHandler
    {

        public override ScreenType Type => ScreenType.Battle;

        private HealthBar _healthBar;

        private void Awake() =>
            _healthBar = GetComponentInChildren<HealthBar>();

        private void OnEnable() => EventBus<BattleScreen>.AddListener(this);

        private void OnDisable() => EventBus<BattleScreen>.RemoveListener(this);

        public void SwitchWeaponUI(AircraftDataModel dataModel)
        {
        }

        public void RefreshUI(AircraftDataModel dataModel)
        {
            print("RefreshUI Invoked");
            _healthBar.Config(dataModel.CurrentHp, dataModel.Hp);
        }

        public void DamageUI(AircraftDataModel dataModel)
        {
            _healthBar.Config(dataModel.CurrentHp, dataModel.Hp);
        }
    }
}