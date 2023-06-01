using System;
using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;
    [SerializeField] private MoveSelector _moveSelector;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _unitPanel;

    
    private string _description;
    private float _currentMana;
    private float _maxMana;
    private Unit _currentUnit;

    public string Name { get; private set; }

    private void Start()
    {
        ShowUnitInfo();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ShowUnitInfo;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ShowUnitInfo;
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

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }

    private void ShowUnitInfo()
    {
        ShowDescription();
        ShowMana();
    }
}
