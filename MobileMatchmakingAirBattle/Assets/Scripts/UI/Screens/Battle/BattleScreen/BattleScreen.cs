using Core;
using Core.Base;
using Enums;
using Interfaces.EventBus;
using TO;
using UI.Screens.Battle.BattleScreen.Elements;
using UI.Screens.Battle.BattleScreen.Elements.Stats;

namespace UI.Screens.Battle.BattleScreen
{
    public class BattleScreen : BaseScreen, IBattleScreenEvents
    {
        public override ScreenType Type => ScreenType.Battle;

        private HealthBar _healthBar;
        private StatsBar _statsBar;
        private BattleScreenInputHandler _battleScreenInput;

        #region UNITY

        void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _statsBar = GetComponentInChildren<StatsBar>();
            _statsBar.gameObject.SetActive(false);
            _battleScreenInput = GetComponent<BattleScreenInputHandler>();
        }

        void OnEnable() => EventBus.AddListener<IBattleScreenEvents>(this);

        void OnDisable() => EventBus.RemoveListener<IBattleScreenEvents>(this);

        void Update()
        {
            _statsBar.gameObject.SetActive(_battleScreenInput.Actions.BattleScreenActions.TabPress.inProgress);
        }

        #endregion

        #region PUBLIC

        public void RefreshUI(AircraftDataModel dataModel)
        {
            _healthBar.Config(dataModel.CurrentHp, dataModel.Hp);
        }

        public void DamageUI(AircraftDataModel dataModel)
        {
            _healthBar.Config(dataModel.CurrentHp, dataModel.Hp);
        }

        #endregion
    }
}