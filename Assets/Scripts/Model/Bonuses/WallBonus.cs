using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Effect))]

public class WallBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;

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

    public override void TakeBonus(Unit unit)
    {
        GameObject gameplayRuler = GameObject.FindGameObjectWithTag("PieceTurnMover");
        PieceTurnMover pieceTurnMover = gameplayRuler.GetComponent<PieceTurnMover>();
        pieceTurnMover.OtherPlayer.AddItem(gameObject);
    }
}
