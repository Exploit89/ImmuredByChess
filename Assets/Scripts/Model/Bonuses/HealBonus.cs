using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Effect))]

public class HealBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;

    private float _healMultiplier = 0.5f;

    public override void TakeBonus(Unit unit)
    {
        unit.Heal(_healMultiplier * unit.MaxHealth);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            TakeBonus(unit);
            BonusTaken?.Invoke();
            GetComponent<Effect>().Activate();
            gameObject.SetActive(false);
        }
    }
}
