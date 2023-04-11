using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    [SerializeField] private GameObject _moveLocationPrefab;
    [SerializeField] private GameObject _tileHighlightPrefab;
    [SerializeField] private GameObject _attackLocationPrefab;
    [SerializeField] private PiecesCreator _piecesCreator;

    private PointConverter _gridPoints;
    private GameObject _tileHighlight;
    private GameObject _movingPiece;
    private List<Vector2Int> _moveLocations;
    private List<GameObject> _locationHighlights;

    void Start()
    {
        _gridPoints = new PointConverter();
        Vector2Int zeroVector = new Vector2Int(0,0);
        enabled = false;
        Debug.Log(_tileHighlightPrefab);
        Debug.Log(_gridPoints.PointFromGrid(zeroVector));
        Debug.Log(gameObject.transform);
        _tileHighlight = Instantiate(_tileHighlightPrefab, _gridPoints.PointFromGrid(zeroVector), Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = _gridPoints.GridFromPoint(point);

            _tileHighlight.SetActive(true);
            _tileHighlight.transform.position = _gridPoints.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                if (!_moveLocations.Contains(gridPoint))
                {
                    return;
                }

                if (_piecesCreator.PieceAtGrid(gridPoint) == null)
                {
                    _piecesCreator.Move(_movingPiece, gridPoint);
                }
                else
                {
                    _piecesCreator.CapturePieceAt(gridPoint);
                    _piecesCreator.Move(_movingPiece, gridPoint);
                }
                ExitState();
            }
        }
        else
        {
            _tileHighlight.SetActive(false);
        }
    }

    private void CancelMove()
    {
        enabled = false;

        foreach (GameObject highlight in _locationHighlights)
        {
            Destroy(highlight);
        }

        _piecesCreator.DeselectPiece(_movingPiece);
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }

    public void EnterState(GameObject piece)
    {
        _movingPiece = piece;
        enabled = true;

        _moveLocations = _piecesCreator.MovesForPiece(_movingPiece);
        _locationHighlights = new List<GameObject>();

        if (_moveLocations.Count == 0)
        {
            CancelMove();
        }

        foreach (Vector2Int loc in _moveLocations)
        {
            GameObject highlight;
            if (_piecesCreator.PieceAtGrid(loc))
            {
                highlight = Instantiate(_attackLocationPrefab, _gridPoints.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(_moveLocationPrefab, _gridPoints.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            _locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        enabled = false;
        TileSelector selector = GetComponent<TileSelector>();
        _tileHighlight.SetActive(false);
        _piecesCreator.DeselectPiece(_movingPiece);
        _movingPiece = null;
        _piecesCreator.NextPlayer();
        selector.EnterState();

        foreach (GameObject highlight in _locationHighlights)
        {
            Destroy(highlight);
        }
    }
}
