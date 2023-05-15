using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Piece))]

public class Unit : MonoBehaviour
{
    private Piece _piece;
    private UnitRank _unitRank;
    private List<Skill> _unitSkills;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Rank UnitRank { get; private set; }
    public int Level { get; private set; } = 1;
    public float Health { get; private set; } = 20f;
    public float MaxHealth { get; private set; } = 20f;
    public float Mana { get; private set; } = 100f;
    public float MaxMana { get; private set; } = 100f;
    public int Experience { get; private set; } = 0;

    public event UnityAction<float, float> HealthChanged;

    void Start()
    {
        _unitRank = gameObject.AddComponent<UnitRank>();
    }
    private void OnEnable()
    {
        _unitSkills = new List<Skill>();
        GameObject skillsObject = GameObject.FindGameObjectWithTag("Skills");
        BaseAttack baseAttack = skillsObject.GetComponentInChildren<BaseAttack>();
        _piece = GetComponent<Piece>();
        Name = _piece.Type.ToString();
        UnitRank = Rank.Basic;
        _unitSkills.Add(baseAttack);
        Description = "description";
        Health = MaxHealth;
        Mana = MaxMana;
        HealthChanged?.Invoke(Health, MaxHealth);
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

    public void IncreaseExperience(int value)
    {
        Experience+= value;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthChanged?.Invoke(Health, MaxHealth);
    }

    public void Heal(float heal)
    {
        Health += heal;
    }

    public void Attack(GameObject pieceToCapture)
    {
        _unitSkills[0].Activate(gameObject.GetComponent<Unit>());
        float damage = _unitSkills[0].GetValueAmount();
        pieceToCapture.GetComponent<Unit>().TakeDamage(damage);
    }

    public void LoadUnitSetup(Rank unitRank, int unitLevel, int maxHealth, int maxMana)
    {
        UnitRank = unitRank;
        Level = unitLevel;
        MaxHealth = maxHealth;
        Health = MaxHealth;
        MaxMana = maxMana;
        Mana = MaxMana;
        _unitSkills.Clear();
        GameObject skillsObject = GameObject.FindGameObjectWithTag("Skills");
        Transform[] transforms = skillsObject.GetComponentsInChildren<Transform>();

        //
        for (int i = 0; i < transforms.Length; i++)
        {
            _unitSkills.Add(transforms[i + 1].gameObject.GetComponent<Skill>());
        }
        // how to do special adding skills???
    }
}
