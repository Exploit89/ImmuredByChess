using System;
using TMPro;
using UnityEngine;

public class UnitManaView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        ChangeManaText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeManaText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeManaText;
    }

    private void ChangeManaText()
    {
        string currentMana = Convert.ToString(GetUnit().Mana);
        string maxMana = Convert.ToString(GetUnit().MaxMana);
        GetComponent<TMP_Text>().text = "Health = " + currentMana + " / " + maxMana;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
