using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Piece))]

public class Unit : MonoBehaviour
{
    private Piece _piece;
    private UnitRank _unitRank;
    private string _name;
    private string _description;
    private float _health;
    private float _mana;
    private List<Skill> _unitSkills;

    public Rank UnitRank { get; private set; }
    public int Level { get; private set; }

    private void OnEnable()
    {
        _unitSkills = new List<Skill>();
        GameObject skillsObject = GameObject.FindGameObjectWithTag("Skills");
        BaseAttack baseAttack = skillsObject.GetComponentInChildren<BaseAttack>();
        _piece = GetComponent<Piece>();
        _unitRank = gameObject.AddComponent<UnitRank>();
        _name = _piece.Type.ToString();
        UnitRank = Rank.Basic;
        _unitSkills.Add(baseAttack);
    }

    public void RiseRank(Rank currentRank)
    {
        foreach (Rank rank in Enum.GetValues(typeof(Rank)))
        {
            if (currentRank == rank)
            {
                int currentRankIndex = _unitRank.GetRankNames().IndexOf(rank);
                int newRankIndex = currentRankIndex + 1;
                Rank newRank = _unitRank.GetRankNames()[newRankIndex];
                UnitRank = newRank;
            }
        }
    }

    public void IncreaseLevel()
    {
        Level++;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void Heal(float heal)
    {
        _health += heal;
    }

    public void Attack()
    {
        _unitSkills[0].Activate(gameObject.GetComponent<Unit>());
    }
}
