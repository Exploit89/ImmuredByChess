using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    private List<int> _levels;
    private int _currentLevel = 1;
    private int _maxLevel = 100;

    private void Awake()
    {
        CreateLevels();
        _pieceTurnMover.GameLevelCompleted += IncreaseLevel;
    }

    private void OnDisable()
    {
        _pieceTurnMover.GameLevelCompleted -= IncreaseLevel;
    }

    private void CreateLevels()
    {
        _levels = new List<int>();

        for (int i = 1; i <= _maxLevel; i++)
        {
            _levels.Add(i);
        }
    }

    private void IncreaseLevel()
    {
        _currentLevel++;
    }

    public int GetUnitLevel()
    {
        int unitLevel = 0;
        return unitLevel;
    }

    public Rank GetUnitRank()
    {
        Rank unitLevel = Rank.Basic;
        return unitLevel;
    }

    public Skill GetUnitSkill()
    {
        Skill unitSkill = new BaseAttack(); //
        return unitSkill;
    }

    public void CreateEnemySetup()
    {

    }
}
