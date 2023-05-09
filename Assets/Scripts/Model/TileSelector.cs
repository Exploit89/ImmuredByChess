using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private GameObject _tileHighlightPrefab;
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    private GameObject _tileHighlight;
    private PointConverter _gridPoints;
    private Vector3 _gridOffset = new Vector3(0.5f, 0, 0.5f);
    private GameObject _selectedPiece;

    void Start()
    {
        _gridPoints = new PointConverter();
        Vector2Int gridPoint = _gridPoints.GridPoint(0, 0);
        Vector3 point = _gridPoints.PointFromGrid(gridPoint);
        _tileHighlight = Instantiate(_tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
        _selectedPiece = new GameObject();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point + _gridOffset;
            Vector2Int gridPoint = _gridPoints.GridFromPoint(point);
            _tileHighlight.SetActive(true);
            _tileHighlight.transform.position = _gridPoints.PointFromGrid(gridPoint);

            if (Input.GetMouseButtonDown(0))
            {
                _selectedPiece = _pieceTurnMover.PieceAtGrid(gridPoint);

                if (_pieceTurnMover.IsPieceBelongToCurrentPlayer(_selectedPiece))
                {
                    _pieceTurnMover.SelectPiece(_selectedPiece);
                    ExitState(_selectedPiece);
                }
            }
        }
        else
        {
            _tileHighlight.SetActive(false);
        }
    }

    public void EnterState()
    {
        enabled = true;
    }

    private void ExitState(GameObject movingPiece)
    {
        enabled = false;
        _tileHighlight.SetActive(false);
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }
}
