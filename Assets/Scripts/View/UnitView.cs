using System;
using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;
    [SerializeField] private MoveSelector _moveSelector;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private ExperienceCalculator _experienceCalculator;
    [SerializeField] private GameObject _unitPanel;

    private string _name;
    private string _description;
    private int _level;
    private int _currentExperience;
    private int _nextLevelExperience;
    private Unit _currentUnit;

    private void Start()
    {
        ShowName();
        ShowExperience();
        ShowLevel();
        ShowDescription();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ShowName;
        _tileSelector.PieceSelected += ShowExperience;
        _tileSelector.PieceSelected += ShowLevel;
        _tileSelector.PieceSelected += ShowDescription;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ShowName;
        _tileSelector.PieceSelected -= ShowExperience;
        _tileSelector.PieceSelected -= ShowLevel;
        _tileSelector.PieceSelected -= ShowDescription;
    }

    private void ShowName()
    {
        _currentUnit = GetUnit();
        _name = _currentUnit.Name;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitName")
                text.text = _name;
        }
    }

    private void ShowLevel()
    {
        _currentUnit = GetUnit();
        _level = _currentUnit.Level;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitLevel")
                text.text = Convert.ToString(_level);
        }
    }

    private void ShowExperience()
    {
        _currentUnit = GetUnit();
        _currentExperience = _currentUnit.Experience;
        _nextLevelExperience = _experienceCalculator.GetNextLevelExperience(_currentUnit.Level);
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitExperience")
                text.text = Convert.ToString(_currentExperience) + " / " + Convert.ToString(_nextLevelExperience);
        }
    }

    private void ShowDescription()
    {
        _currentUnit = GetUnit();
        _description = _currentUnit.Description;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitDescription")
                text.text = Convert.ToString(_description);
        }
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
