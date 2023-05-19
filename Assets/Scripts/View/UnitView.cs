using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;
    [SerializeField] private MoveSelector _moveSelector;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private ExperienceCalculator _experienceCalculator;
    [SerializeField] private GameObject _unitPanel;
    [SerializeField] private List<Toggle> _toggles;

    private string _name;
    private string _description;
    private int _level;
    private int _currentExperience;
    private int _nextLevelExperience;
    private float _currentHealth;
    private float _maxHealth;
    private float _currentMana;
    private float _maxMana;
    private Unit _currentUnit;

    private void Start()
    {
        ShowName();
        ShowExperience();
        ShowLevel();
        ShowDescription();
        ShowHealth();
        ShowMana();
        ShowSkills();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ShowName;
        _tileSelector.PieceSelected += ShowExperience;
        _tileSelector.PieceSelected += ShowLevel;
        _tileSelector.PieceSelected += ShowDescription;
        _tileSelector.PieceSelected += ShowHealth;
        _tileSelector.PieceSelected += ShowMana;
        _tileSelector.PieceSelected += ShowSkills;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ShowName;
        _tileSelector.PieceSelected -= ShowExperience;
        _tileSelector.PieceSelected -= ShowLevel;
        _tileSelector.PieceSelected -= ShowDescription;
        _tileSelector.PieceSelected -= ShowHealth;
        _tileSelector.PieceSelected -= ShowMana;
        _tileSelector.PieceSelected -= ShowSkills;
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

    private void ShowHealth()
    {
        _currentUnit = GetUnit();
        _currentHealth = _currentUnit.Health;
        _maxHealth = _currentUnit.MaxHealth;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitHealth")
                text.text = "Health = " + Convert.ToString(_currentHealth) + " / " + Convert.ToString(_maxHealth);
        }
    }

    private void ShowMana()
    {
        _currentUnit = GetUnit();
        _currentMana = _currentUnit.Mana;
        _maxMana = _currentUnit.MaxMana;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "UnitMana")
                text.text = "Mana = " + Convert.ToString(_currentMana) + " / " + Convert.ToString(_maxMana);
        }
    }

    private void ShowSkills()
    {
        _currentUnit = GetUnit();
        _toggles[1].GetComponent<Toggle>().isOn = false;
        _toggles[2].GetComponent<Toggle>().isOn = false;
        _toggles[0].GetComponent<Toggle>().isOn = true;

        for (int i = 0; i < _currentUnit.GetUnitSkills().Count; i++)
        {
            _toggles[i].GetComponent<SkillToggle>().SetName(_currentUnit.GetUnitSkills()[i].name);
        }
        _tileSelector.PieceSelected -= ShowSkills;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
