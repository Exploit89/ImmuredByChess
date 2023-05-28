using UnityEngine;
using UnityEngine.Events;

public class HealBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;

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
            GetComponent<Effect>().ActivateEffect();
            gameObject.SetActive(false);
        }
    }
}
