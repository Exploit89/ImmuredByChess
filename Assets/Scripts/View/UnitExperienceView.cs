using System;
using TMPro;
using UnityEngine;

public class UnitExperienceView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;
    [SerializeField] private ExperienceCalculator _experienceCalculator;

    private void Start()
    {
        ChangeExperienceText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeExperienceText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeExperienceText;
    }

    private void ChangeExperienceText()
    {
        string currentExperience = Convert.ToString(GetUnit().Experience);
        string nextLevelExperience = Convert.ToString((_experienceCalculator.GetNextLevelExperience(GetUnit().Level)));
        GetComponent<TMP_Text>().text = currentExperience + " / " + nextLevelExperience;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
