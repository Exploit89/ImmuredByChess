using TMPro;
using UnityEngine;

public class UnitDescriptionView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        ChangeDescriptionText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeDescriptionText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeDescriptionText;
    }

    private void ChangeDescriptionText()
    {
        GetComponent<TMP_Text>().text = GetUnit().Description;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
