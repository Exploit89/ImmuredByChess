using TMPro;
using UnityEngine;

public class UnitLabelView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        ChangeLabelText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeLabelText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeLabelText;
    }

    private void ChangeLabelText()
    {
        GetComponent<TMP_Text>().text = GetUnit().Name;
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
