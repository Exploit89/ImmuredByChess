using UnityEngine;
using UnityEngine.UI;

public class PieceHitPointView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Unit _unit;

    void OnEnable()
    {
        _unit = GetComponent<Unit>();
        _unit.HealthChanged += OnValueChanged;
        _slider.value = 1;
    }

    void OnDisable()
    {
        _unit.HealthChanged -= OnValueChanged;
    }

    public void OnValueChanged(float value, float maxvalue)
    {
        _slider.value = value / maxvalue;
    }
}
