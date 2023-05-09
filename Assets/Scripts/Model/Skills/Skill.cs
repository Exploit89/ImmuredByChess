using UnityEngine;

public enum SkillType
{
    Buff,
    Debuff,
    Attack,
    Heal,
    Dot
}

public abstract class Skill : MonoBehaviour
{
    public abstract float MultiplieValue(float baseValue, int unitLevel);
    public abstract void Activate(Unit unit);
    public abstract float GetValueAmount();
}
