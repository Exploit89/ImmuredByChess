using UnityEngine;
using UnityEngine.Events;

public class WallBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            TakeBonus(unit);
            BonusTaken?.Invoke();

            if (TryGetComponent(out Effect effect))
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
