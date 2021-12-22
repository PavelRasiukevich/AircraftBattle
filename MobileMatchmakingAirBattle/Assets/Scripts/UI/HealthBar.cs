using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour, IObserver
    {
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;
        [SerializeField] private Slider _slider;

        private void Awake() => SetInitialValues();

        private void SetInitialValues()
        {
            _slider.value = 1.0f;
            _fill.color = _gradient.Evaluate(1.0f);
        }

        private void ChangeHealthAmount(float currentHealthValue, float max)
        {
            _slider.value = currentHealthValue / max;
            _fill.color = _gradient.Evaluate(_slider.value);
        }

        public void PerformAction(float cur, float max) => ChangeHealthAmount(cur, max);
    }
}