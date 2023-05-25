using UnityEngine;
using UnityEngine.UI;

public class PieceHitPointView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Board _board;
    private PiecesCreator _piecesCreator;
    private GameObject _piece;
    private Unit _unit;

    private void OnEnable()
    {
        _slider.value = 1;
    }

    private void OnDestroy()
    {
        _unit.HealthChanged -= OnValueChanged;
        _board.PieceMoved -= MoveHealthBar;
        _piecesCreator.PieceMoved -= MoveHealthBar;
    }

    public void OnValueChanged(float value, float maxvalue)
    {
        _slider.value = value / maxvalue;
    }

    public void SetPiece(GameObject piece)
    {
        _piece = piece;
    }

    public void AddListener()
    {
        _unit = _piece.GetComponent<Unit>();
        GameObject board = GameObject.FindGameObjectWithTag("Board");
        GameObject pieceCreator = GameObject.FindGameObjectWithTag("PiecesCreator");
        _board = board.GetComponent<Board>();
        _piecesCreator = pieceCreator.GetComponent<PiecesCreator>();
        _unit.HealthChanged += OnValueChanged;
        _board.PieceMoved += MoveHealthBar;
        _piecesCreator.PieceMoved += MoveHealthBar;
        _slider.value = 1;
    }

    public void MoveHealthBar()
    {
        gameObject.transform.position = _piece.transform.position;
        gameObject.transform.Translate(0, 1, -1);
    }
}
