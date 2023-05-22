using System.Collections.Generic;
using UnityEngine;

public class GameLevelSetup
{
    private int _baseHealth = 10;
    private int _baseMana = 10;

    public PieceType Piece { get; private set; }
    public Rank UnitRank { get; private set; }
    public int Level { get; private set; }
    public int MaxHealth { get; private set; }
    public int MaxMana { get; private set; }

    private void GetBasicSkills(List<Skill> skills, Transform[] transforms)
    {
        skills.Add(transforms[1].gameObject.GetComponent<Skill>());
    }

    private void GetExpertSKills(List<Skill> skills, Transform[] transforms)
    {
        switch (Piece)
        {
            case PieceType.King:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[2].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Queen:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[3].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Bishop:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[4].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Knight:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[5].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Rook:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[6].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Pawn:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[7].gameObject.GetComponent<Skill>());
                break;
            default:
                Debug.Log("can't find any PieceType");
                break;
        }
    }

    private void GetGrandmasterSKills(List<Skill> skills, Transform[] transforms)
    {
        switch (Piece)
        {
            case PieceType.King:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[2].gameObject.GetComponent<Skill>());
                skills.Add(transforms[8].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Queen:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[3].gameObject.GetComponent<Skill>());
                skills.Add(transforms[9].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Bishop:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[4].gameObject.GetComponent<Skill>());
                skills.Add(transforms[10].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Knight:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[5].gameObject.GetComponent<Skill>());
                skills.Add(transforms[11].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Rook:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[6].gameObject.GetComponent<Skill>());
                skills.Add(transforms[12].gameObject.GetComponent<Skill>());
                break;
            case PieceType.Pawn:
                skills.Add(transforms[1].gameObject.GetComponent<Skill>());
                skills.Add(transforms[7].gameObject.GetComponent<Skill>());
                skills.Add(transforms[13].gameObject.GetComponent<Skill>());
                break;
            default:
                Debug.Log("can't find any PieceType");
                break;
        }
    }

    public List<Skill> GetSkills()
    {
        List<Skill> skills = new List<Skill>();
        GameObject skillsObject = GameObject.FindGameObjectWithTag("Skills");
        Transform[] transforms = skillsObject.GetComponentsInChildren<Transform>();

        switch (UnitRank)
        {
            case Rank.Basic:
                GetBasicSkills(skills, transforms);
                break;
            case Rank.Advanced:
                GetBasicSkills(skills, transforms);
                break;
            case Rank.Expert:
                GetExpertSKills(skills, transforms);
                break;
            case Rank.Master:
                GetExpertSKills(skills, transforms);
                break;
            case Rank.Grandmaster:
                GetGrandmasterSKills(skills, transforms);
                break;
            default:
                Debug.Log("can't find any UnitRank"); 
                break;
        }
        return skills;
    }

    private Rank GetUnitRank()
    {
        Rank rank = new Rank();

        if(Level <= 10)
            rank = Rank.Basic;
        else if(Level <= 30 && Level > 10)
            rank = Rank.Advanced;
        else if(Level <= 50 && Level > 30)
            rank = Rank.Expert;
        else if (Level <= 70 && Level > 50)
            rank = Rank.Master;
        else if (Level > 70)
            rank = Rank.Grandmaster;
        return rank;
    }

    private int GetHealthManaModifier()
    {
        int modifier = 0;

        switch (UnitRank)
        {
            case Rank.Advanced:
                modifier = 2;
                break;
            case Rank.Master:
                modifier = 3;
                break;
            default:
                modifier = 1;
                break;
        }

        return modifier;
    }

    public GameLevelSetup(int gameLevel, PieceType pieceType)
    {
        Piece = pieceType;
        Level = gameLevel;
        UnitRank = GetUnitRank();
        MaxHealth = _baseHealth * gameLevel * GetHealthManaModifier();
        MaxMana = _baseMana * gameLevel * GetHealthManaModifier();
    }
}
