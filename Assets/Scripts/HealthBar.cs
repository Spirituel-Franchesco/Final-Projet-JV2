using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
