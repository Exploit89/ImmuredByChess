using System.Collections.Generic;
using UnityEngine;

public class ExperienceCalculator : MonoBehaviour
{
    private List<int> _levelExperience = new List<int>();
    private Dictionary<PieceType, int> _pieceTypeExperienceReward = new Dictionary<PieceType, int>();
    private int _experienceStep = 100;
    private int _kingReward = 200;
    private int _queenReward = 100;
    private int _bishopReward = 30;
    private int _knightReward = 30;
    private int _rookReward = 50;
    private int _pawnReward = 10;

    public int MaxLevel { get; private set; } = 100;

    private void Awake()
    {
        _pieceTypeExperienceReward.Clear();
        _pieceTypeExperienceReward.Add(PieceType.King, _kingReward);
        _pieceTypeExperienceReward.Add(PieceType.Queen, _queenReward);
        _pieceTypeExperienceReward.Add(PieceType.Bishop, _bishopReward);
        _pieceTypeExperienceReward.Add(PieceType.Knight, _knightReward);
        _pieceTypeExperienceReward.Add(PieceType.Rook, _rookReward);
        _pieceTypeExperienceReward.Add(PieceType.Pawn, _pawnReward);

        for (int i = 0; i < MaxLevel; i++)
        {
            _levelExperience.Add(_experienceStep * i * i);
        }
    }

    public int GetNextLevelExperience(int level)
    {
        return _levelExperience[level + 1];
    }

    public int GetExperienceReward(PieceType pieceType)
    {
        int reward = 0;

        foreach (var piece in _pieceTypeExperienceReward.Keys)
        {
            if (piece == pieceType)
                reward = _pieceTypeExperienceReward[pieceType];
        }
        return reward;
    }

    public bool IsPlayerLevelReached(int experience, int level)
    {
        bool levelReached = false;

        for (int i = level; experience >= _levelExperience[i]; i++)
        {
            levelReached = true;
        }
        return levelReached;
    }

    public bool IsUnitLevelReached(int experience, int level)
    {
        bool levelReached = false;

        for (int i = level; experience >= _levelExperience[i]; i++)
        {
            levelReached = true;
        }
        return levelReached;
    }
}
