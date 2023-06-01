using UnityEngine;

public enum SkillType
{
    Buff,
    Debuff,
    Attack,
    Heal,
    Dot
}

public enum SkillGrade
{
    Basic,
    Middle,
    Expert
}

public enum SkillClass
{
    King,
    Queen,
    Bishop,
    Knight,
    Rook,
    Pawn
}

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private protected SkillClass _pieceSkillClass;

    public SkillClass PieceSkillClass => _pieceSkillClass;

    public abstract float MultiplieValue(float baseValue, int unitLevel);
    public abstract void Activate(Unit unit);
    public abstract float GetValueAmount();
}
