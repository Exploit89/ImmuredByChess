using UnityEngine;

public class HealBonus : Bonus
{
    private GameLevel _gameLevel;

    private void Start()
    {
        GameObject gameLevel = GameObject.FindGameObjectWithTag("GameLevel");
        _gameLevel = gameLevel.GetComponent<GameLevel>();
    }

    public override void TakeBonus(Unit unit)
    {
        int heal = 5;
        int currentGameLevel = _gameLevel.GetCurrentLevel();
        unit.Heal(heal * currentGameLevel * 0.01f);
        gameObject.SetActive(false);
    }
}
