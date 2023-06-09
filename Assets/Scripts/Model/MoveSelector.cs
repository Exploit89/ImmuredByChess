using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    [SerializeField] private GameObject _moveLocationPrefab;
    [SerializeField] private GameObject _tileHighlightPrefab;
    [SerializeField] private GameObject _attackLocationPrefab;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _unitPanel;
    [SerializeField] private BonusCreator _bonusCreator;

    private PointConverter _gridPoints;
    private GameObject _tileHighlight;
    private GameObject _movingPiece;
    private List<Vector2Int> _moveLocations;
    private List<GameObject> _locationHighlights;
    private Vector3 _gridOffset = new Vector3(0.5f, 0, 0.5f);

    void Start()
    {
        _moveLocations = new List<Vector2Int>();
        _gridPoints = new PointConverter();
        Vector2Int zeroVector = new Vector2Int(0, 0);
        enabled = false;
        _tileHighlight = Instantiate(_tileHighlightPrefab, _gridPoints.PointFromGrid(zeroVector), Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
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
                if (!_moveLocations.Contains(gridPoint))
                {
                    CancelState();
                    return;
                }

                if (_pieceTurnMover.PieceAtGrid(gridPoint) == null && _bonusCreator.IsTileClearFromAbility(gridPoint))
                {
                    _pieceTurnMover.Move(_movingPiece, gridPoint);
                }
                else if (_pieceTurnMover.PieceAtGrid(gridPoint) != null)
                {
                    GameObject target = _pieceTurnMover.PieceAtGrid(gridPoint);
                    _movingPiece.GetComponent<Unit>().Attack(target);
                    _pieceTurnMover.TryDestroyTarget(gridPoint);
                    _pieceTurnMover.TryMove(_movingPiece, gridPoint);
                }
                else
                {
                    CancelState();
                    return;
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
        _pieceTurnMover.DeselectPiece(_movingPiece);
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }

    public void EnterState(GameObject piece)
    {
        _movingPiece = piece;
        enabled = true;
        _moveLocations = _pieceTurnMover.MovesForPiece(_movingPiece);
        _locationHighlights = new List<GameObject>();

        if (_moveLocations.Count == 0)
            CancelMove();

        foreach (Vector2Int location in _moveLocations)
        {
            GameObject highlight;

            if (_pieceTurnMover.PieceAtGrid(location))
                highlight = Instantiate(_attackLocationPrefab, _gridPoints.PointFromGrid(location), Quaternion.identity, gameObject.transform);
            else
                highlight = Instantiate(_moveLocationPrefab, _gridPoints.PointFromGrid(location), Quaternion.identity, gameObject.transform);
            _locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        if (_pieceTurnMover.IsSetupRestarted)
        {
            _pieceTurnMover.TurnOffSetupRestarted();
            return;
        }
        enabled = false;
        TileSelector selector = GetComponent<TileSelector>();
        _tileHighlight.SetActive(false);
        _pieceTurnMover.DeselectPiece(_movingPiece);
        _movingPiece = null;
        _pieceTurnMover.NextPlayer();
        selector.EnterState();
        _unitPanel.SetActive(false);

        foreach (GameObject highlight in _locationHighlights)
        {
            Destroy(highlight);
        }
    }

    public void CancelState()
    {
        enabled = false;
        TileSelector selector = GetComponent<TileSelector>();
        _tileHighlight.SetActive(false);
        _pieceTurnMover.DeselectPiece(_movingPiece);
        _movingPiece = null;
        selector.EnterState();
        _unitPanel.SetActive(false);

        foreach (GameObject highlight in _locationHighlights)
        {
            Destroy(highlight);
        }
    }
}
