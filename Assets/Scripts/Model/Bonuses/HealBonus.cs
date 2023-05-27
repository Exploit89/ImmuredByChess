using UnityEngine;
using UnityEngine.Events;

public class HealBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;
    [SerializeField] private GameObject _bonusPrefab;

    private GameObject _bonusEffect;

    public override void TakeBonus(Unit unit)
    {
        float heal = 0.5f;
        unit.Heal(heal * unit.MaxHealth);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            TakeBonus(unit);
            BonusTaken?.Invoke();
            _bonusEffect.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void CreateEffect()
    {
        _bonusEffect = Instantiate(_bonusPrefab);
        _bonusEffect.SetActive(false);
    }

    public void SetEffectPosition(Vector3 position)
    {
        _bonusEffect.transform.position = position;
    }
}
