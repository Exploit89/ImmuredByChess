using System.Collections.Generic;
using UnityEngine;

public class GameLevelSetup
{
    private List<Skill> _skillList;
    private int _baseHealth = 20;
    private int _baseMana = 20;
    private int _advancedLevel = 10;
    private int _expertLevel = 30;
    private int _healthManaModifier = 0;
    private int _baseHealthManaModifier = 1;
    private int _advancedHealthManaModifier = 2;
    private int _expertHealthManaModifier = 3;

    public PieceType Piece { get; private set; }
    public Rank UnitRank { get; private set; }
    public int Level { get; private set; }
    public int MaxHealth { get; private set; }
    public int MaxMana { get; private set; }

    private void GetBasicSkills(List<Skill> skills)
    {
        skills.Add(_skillList[0]);
    }

    private void GetExpertSkills(List<Skill> skills)
    {
        GetAvaliableSkill(skills, Piece);
    }

    public List<Skill> GetSkills()
    {
        List<Skill> skillList = new List<Skill>();
        switch (UnitRank)
        {
            case Rank.Basic:
                GetBasicSkills(skillList);
                break;
            case Rank.Advanced:
                GetBasicSkills(skillList);
                break;
            case Rank.Expert:
                GetExpertSkills(skillList);
                break;
            default:
                Debug.Log("can't find any UnitRank");
                break;
        }
        return skillList;
    }

    private Rank GetUnitRank()
    {
        Rank rank = new Rank();

        if (Level <= _advancedLevel)
            rank = Rank.Basic;
        else if (Level <= _expertLevel && Level > _advancedLevel)
            rank = Rank.Advanced;
        else if (Level > _expertLevel)
            rank = Rank.Expert;
        return rank;
    }

    private int GetHealthManaModifier()
    {
        _healthManaModifier = 0;

        switch (UnitRank)
        {
            case Rank.Advanced:
                _healthManaModifier = _advancedHealthManaModifier;
                break;
            case Rank.Master:
                _healthManaModifier = _expertHealthManaModifier;
                break;
            default:
                _healthManaModifier = _baseHealthManaModifier;
                break;
        }
        return _healthManaModifier;
    }

    private void GetAvaliableSkill(List<Skill> skills, PieceType pieceType)
    {
        for (int i = 1; i < skills.Count; i++)
        {
            if (skills[i].GetComponent<Skill>().PieceSkillClass.ToString() == pieceType.ToString())
            {
                GetBasicSkills(_skillList);
                skills.Add(skills[i].GetComponent<Skill>());
            }
        }
    }

    public GameLevelSetup(int gameLevel, PieceType pieceType, List<Skill> skills)
    {
        _skillList = new List<Skill>();
        _skillList = skills;
        Piece = pieceType;
        Level = gameLevel;
        UnitRank = GetUnitRank();
        MaxHealth = _baseHealth * gameLevel * GetHealthManaModifier();
        MaxMana = _baseMana * gameLevel * GetHealthManaModifier();
    }
}
