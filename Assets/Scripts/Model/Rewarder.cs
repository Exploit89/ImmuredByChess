using System.Collections.Generic;
using UnityEngine;

public class Rewarder : MonoBehaviour
{
    private Dictionary<PieceType, int> _pieceTypeMoneyReward = new Dictionary<PieceType, int>();
    private Dictionary<Rank, int> _rankRewardMultiplier = new Dictionary<Rank, int>();

    private void Awake()
    {
        _pieceTypeMoneyReward.Clear();
        _pieceTypeMoneyReward.Add(PieceType.King, 100);
        _pieceTypeMoneyReward.Add(PieceType.Queen, 50);
        _pieceTypeMoneyReward.Add(PieceType.Bishop, 20);
        _pieceTypeMoneyReward.Add(PieceType.Knight, 20);
        _pieceTypeMoneyReward.Add(PieceType.Rook, 30);
        _pieceTypeMoneyReward.Add(PieceType.Pawn, 5);
        _rankRewardMultiplier.Add(Rank.Basic, 1);
        _rankRewardMultiplier.Add(Rank.Advanced, 2);
        _rankRewardMultiplier.Add(Rank.Expert, 3);
        _rankRewardMultiplier.Add(Rank.Master, 4);
        _rankRewardMultiplier.Add(Rank.Grandmaster, 5);
    }

    public int GetMoneyReward(PieceType pieceType, Rank rank)
    {
        int reward = 0;

        foreach (var piece in _pieceTypeMoneyReward.Keys)
        {
            if (piece == pieceType)
                reward = _pieceTypeMoneyReward[pieceType];
        }
        return reward * _rankRewardMultiplier[rank];
    }
}
