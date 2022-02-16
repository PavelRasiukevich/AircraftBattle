using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Battle.BattleScreen.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;
        [SerializeField] private Slider _slider;

        public void Config(float curr, float max)
        {
            _slider.value = curr / max;
            _fill.color = _gradient.Evaluate(_slider.value);
        }
    }
}