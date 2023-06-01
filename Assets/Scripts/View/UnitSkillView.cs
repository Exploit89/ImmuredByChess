using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSkillView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;
    [SerializeField] private List<Toggle> _toggles;

    private Unit _currentUnit;
    private int _defaultToggleIndex = 0;

    private void Start()
    {
        ShowSkills();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ShowSkills;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ShowSkills;
    }

    private void ShowSkills()
    {
        _currentUnit = GetUnit();
        ResetSkillToggles();

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

    private void ResetSkillToggles()
    {
        for (int i = _toggles.Count; i > 0; i--)
        {
            _toggles[i - 1].isOn = false;
        }
        _toggles[_defaultToggleIndex].isOn = true;
    }
}
