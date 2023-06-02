using System;
using System.Collections.Generic;
using UnityEngine;

public enum Rank
{
    Basic,
    Advanced,
    Expert,
    Master,
    Grandmaster
};

public class UnitRank : MonoBehaviour
{
    private List<Rank> _ranks = new List<Rank>();

    private void Awake()
    {
        CreateRankList();
    }

    private void CreateRankList()
    {
        foreach (Rank rank in Enum.GetValues(typeof(Rank)))
        {
            _ranks.Add(rank);
        }
    }

    public List<Rank> GetRankNames()
    {
        List<Rank> rankNames = new List<Rank>();

        foreach (Rank rank in _ranks)
        {
            rankNames.Add(rank);
        }
        return rankNames;
    }
}
