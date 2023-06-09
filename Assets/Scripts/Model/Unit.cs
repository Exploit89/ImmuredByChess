using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Piece))]

public class Unit : MonoBehaviour
{
    private Piece _piece;
    private UnitRank _unitRank;
    private List<Skill> _unitSkills = new List<Skill>();

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
        GameObject piece = GetComponentInParent<Transform>().gameObject;
        GameObject playerPieces = piece.GetComponentInParent<PiecesGroup>().gameObject;
        GameObject board = playerPieces.GetComponentInParent<Board>().gameObject;
        Skills skills = board.GetComponentInChildren<Skills>();
        BaseAttack baseAttack = skills.GetComponentInChildren<BaseAttack>();

        _piece = GetComponent<Piece>();
        Name = _piece.Type.ToString();
        UnitRank = Rank.Basic;
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
        Experience += value;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthChanged?.Invoke(Health, MaxHealth);
    }

    public void Heal(float heal)
    {
        if (heal + Health >= MaxHealth)
            Health = MaxHealth;
        else Health += heal;
        HealthChanged?.Invoke(Health, MaxHealth);
    }

    public void Attack(GameObject pieceToCapture)
    {
        if (pieceToCapture.TryGetComponent(out Unit unit))
        {
            float damage = 0;

            foreach (var skill in _unitSkills)
            {
                if (skill.TryGetComponent(out BaseAttack baseAttack))
                {
                    skill.Activate(gameObject.GetComponent<Unit>());
                    damage = skill.GetValueAmount();
                }
            }
            pieceToCapture.GetComponent<Unit>().TakeDamage(damage);
        }
    }

    public void Support(GameObject pieceToCapture)
    {
        GameObject currentPiece = gameObject.GetComponentInParent<Transform>().GetComponentInParent<Transform>().gameObject;
        GameObject piece = GetComponentInParent<Transform>().gameObject;
        GameObject playerPieces = piece.GetComponentInParent<PiecesGroup>().gameObject;
        GameObject board = playerPieces.GetComponentInParent<Board>().gameObject;
        PieceTurnMover pieceTurnMover = board.GetComponentInChildren<PieceTurnMover>();

        bool isPlayerCurrent = pieceTurnMover.CurrentPlayer.ContainsPiece(currentPiece);
        float heal = 0;

        if (isPlayerCurrent)
        {
            foreach (var skill in _unitSkills)
            {
                if (skill.TryGetComponent(out Heal healSkill))
                {
                    skill.Activate(gameObject.GetComponent<Unit>());
                    heal = skill.GetValueAmount();
                }
            }
            pieceToCapture.GetComponent<Unit>().Heal(heal);
        }
    }

    public void LoadUnitSetup(GameLevelSetup setup)
    {
        UnitRank = setup.UnitRank;
        Level = setup.Level;
        MaxHealth = setup.MaxHealth;
        MaxHealth = setup.MaxMana;
        Health = MaxHealth;
        Mana = MaxMana;
        _unitSkills.Clear();

        foreach (var skill in setup.GetSkills())
        {
            _unitSkills.Add(skill);
        }
    }

    public List<Skill> GetUnitSkills()
    {
        List<Skill> skills = new List<Skill>();
        skills = _unitSkills;
        return skills;
    }

    public void AddBaseSkill()
    {
        GameObject skillsObject = GameObject.FindGameObjectWithTag("Skills");
        BaseAttack baseAttack = skillsObject.GetComponentInChildren<BaseAttack>();
        _unitSkills.Add(baseAttack);
    }
}
