using UnityEngine;
using UnityEngine.Events;

public class WallBonus : Bonus
{
    [SerializeField] private UnityEvent BonusTaken;

    public override void TakeBonus(Unit unit)
    {
        GameObject gameplayRuler = GameObject.FindGameObjectWithTag("PieceTurnMover");
        PieceTurnMover pieceTurnMover = gameplayRuler.GetComponent<PieceTurnMover>();
        pieceTurnMover.CurrentPlayer.AddItem(gameObject);

        foreach (var item in pieceTurnMover.CurrentPlayer.GetItems())
        {
            Debug.Log(item.name);
        }
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
