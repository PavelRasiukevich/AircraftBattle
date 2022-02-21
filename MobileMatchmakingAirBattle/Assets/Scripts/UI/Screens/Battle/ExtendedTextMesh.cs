using Assets.Scripts.UI.Screens.Battle.BattleScreen.Elements;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Battle
{
    public class ExtendedTextMesh : MonoBehaviour
    {
        [SerializeField] private BattleClock _clock;

        private TextMeshProUGUI _ugui;

        private void Awake()
        {
            _ugui = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _ugui.text = _clock.FormatedTime;
        }
    }
}