using System.Collections.Generic;
using UnityEngine;

public class Rewarder : MonoBehaviour
{
    private Dictionary<PieceType, int> _pieceTypeMoneyReward = new Dictionary<PieceType, int>();
    private Dictionary<Rank, int> _rankRewardMultiplier = new Dictionary<Rank, int>();
    private int _kingReward = 100;
    private int _queenReward = 50;
    private int _bishopReward = 20;
    private int _knightReward = 20;
    private int _rookReward = 30;
    private int _pawnReward = 5;
    private int _rankBasicIndex = 1;
    private int _ranAdvancedndex = 2;
    private int _rankExpertIndex = 3;
    private int _rankMasterIndex = 4;
    private int _rankGrandmasterIndex = 5;


    private void Awake()
    {
        _pieceTypeMoneyReward.Clear();
        _pieceTypeMoneyReward.Add(PieceType.King, _kingReward);
        _pieceTypeMoneyReward.Add(PieceType.Queen, _queenReward);
        _pieceTypeMoneyReward.Add(PieceType.Bishop, _bishopReward);
        _pieceTypeMoneyReward.Add(PieceType.Knight, _knightReward);
        _pieceTypeMoneyReward.Add(PieceType.Rook, _rookReward);
        _pieceTypeMoneyReward.Add(PieceType.Pawn, _pawnReward);
        _rankRewardMultiplier.Add(Rank.Basic, _rankBasicIndex);
        _rankRewardMultiplier.Add(Rank.Advanced, _ranAdvancedndex);
        _rankRewardMultiplier.Add(Rank.Expert, _rankExpertIndex);
        _rankRewardMultiplier.Add(Rank.Master, _rankMasterIndex);
        _rankRewardMultiplier.Add(Rank.Grandmaster, _rankGrandmasterIndex);
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
