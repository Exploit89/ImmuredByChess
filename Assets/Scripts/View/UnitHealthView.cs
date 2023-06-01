using System;
using TMPro;
using UnityEngine;

public class UnitHealthView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        ChangeHealthText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeHealthText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeHealthText;
    }

    private void ChangeHealthText()
    {
        string currentHealth = Convert.ToString(GetUnit().Health);
        string maxHealth = Convert.ToString(GetUnit().MaxHealth);
        GetComponent<TMP_Text>().text = "Health = " + currentHealth + " / " + maxHealth;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
