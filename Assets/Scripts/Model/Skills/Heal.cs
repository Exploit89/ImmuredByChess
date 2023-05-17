using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skill
{
    private string _name;
    private string _description;
    private int _rank;
    private int _baseValue;
    private float _levelMultiplier = 0.1f;
    private int _baseMultiplier = 1;
    private SkillType _skillType;
    private ExperienceCalculator _experienceCalculator;

    public float ValueAmount { get; private set; }

    private void OnEnable()
    {
        _name = "�������";
        _description = "�������, ��������������� ��������";
        _rank = 1;
        _baseValue = 10;
        _skillType = SkillType.Heal;
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

        Debug.Log("����� ��� = " + totalValue); // ����������� ��������� �����
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
