using TMPro;
using UnityEngine;

public class UnitLevelView : MonoBehaviour
{
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        ChangeLevelText();
    }

    private void OnEnable()
    {
        _tileSelector.PieceSelected += ChangeLevelText;
    }

    private void OnDestroy()
    {
        _tileSelector.PieceSelected -= ChangeLevelText;
    }

    private void ChangeLevelText()
    {
        GetComponent<TMP_Text>().text = GetUnit().Level.ToString();
    }

    private Unit GetUnit()
    {
        return _tileSelector.GetSelectedPiece().GetComponentInChildren<Unit>();
    }
}
