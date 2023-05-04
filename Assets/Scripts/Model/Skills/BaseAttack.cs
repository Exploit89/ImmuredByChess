using System.Collections.Generic;
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
    private ExperienceCalculator _experienceCalculator;
    private Unit _unit;

    private void OnEnable()
    {
        _name = "Базовая атака";
        _description = "Обычная атака, наносит прямой урон";
        _rank = 1;
        _baseValue = 10;
        _skillType = SkillType.Attack;
        gameObject.AddComponent<ExperienceCalculator>();
        _experienceCalculator = GetComponent<ExperienceCalculator>();
        _unit = GetComponentInParent<Unit>();
    }

    public override float MultiplieValue(float baseValue, int unitLevel)
    {
        float totalValue = 0;

        if(unitLevel == 1)
            return baseValue;
        else
            totalValue = baseValue * (_baseMultiplier + (unitLevel * _levelMultiplier));

        Debug.Log("тотал дамаг = " + totalValue);
        return totalValue;
    }

    public override void Activate()
    {
        MultiplieValue(_baseValue, _unit.Level);
        // target?
    }
}
