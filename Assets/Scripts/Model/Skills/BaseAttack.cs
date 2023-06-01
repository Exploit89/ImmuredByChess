using UnityEngine;

public class BaseAttack : Skill
{
    private string _name;
    private string _description;
    private int _rank;
    private int _baseValue;
    private float _levelMultiplier = 0.1f;
    private int _baseMultiplier = 1;
    private SkillType _skillType;
    private SkillClass _skillClass;
    private ExperienceCalculator _experienceCalculator;

    public float ValueAmount { get; private set; }

    private void OnEnable()
    {
        _name = "Базовая атака";
        _description = "Обычная атака, наносит прямой урон";
        _rank = 1;
        _baseValue = 10;
        _skillType = SkillType.Attack;
        gameObject.AddComponent<ExperienceCalculator>();
        _experienceCalculator = GetComponent<ExperienceCalculator>();
    }

    public override float MultiplieValue(float baseValue, int unitLevel)
    {
        float totalValue = 0;

        if (unitLevel == 1)
            return baseValue;
        else
            totalValue = baseValue * (_baseMultiplier + (unitLevel * _levelMultiplier));
        return totalValue;
    }

    public override void Activate(Unit unit)
    {
        float totalValue = MultiplieValue(_baseValue, unit.Level);
        ValueAmount = totalValue;
    }

    public override float GetValueAmount()
    {
        return ValueAmount;
    }
}
