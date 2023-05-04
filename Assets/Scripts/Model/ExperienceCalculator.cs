using System.Collections.Generic;
using UnityEngine;

public class ExperienceCalculator : MonoBehaviour
{
    private List<int> _levelExperience = new List<int>();
    private int _experienceStep = 100;
    private Dictionary<PieceType, int> _pieceTypeExperienceReward = new Dictionary<PieceType, int>();

    public int MaxLevel { get; private set; } = 100;

    private void Awake()
    {
        _pieceTypeExperienceReward.Clear();
        _pieceTypeExperienceReward.Add(PieceType.King, 200);
        _pieceTypeExperienceReward.Add(PieceType.Queen, 100);
        _pieceTypeExperienceReward.Add(PieceType.Bishop, 30);
        _pieceTypeExperienceReward.Add(PieceType.Knight, 30);
        _pieceTypeExperienceReward.Add(PieceType.Rook, 50);
        _pieceTypeExperienceReward.Add(PieceType.Pawn, 10);

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
}
