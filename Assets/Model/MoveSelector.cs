using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    [SerializeField] private GameObject _moveLocationPrefab;
    [SerializeField] private GameObject _tileHighlightPrefab;
    [SerializeField] private GameObject _attackLocationPrefab;

    private Geometry _geometry;
    private GameObject _tileHighlight;
    private GameObject _movingPiece;
    private List<Vector2Int> _moveLocations;
    private List<GameObject> _locationHighlights;

    void Start()
    {
        enabled = false;
        _tileHighlight = Instantiate(_tileHighlightPrefab, _geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = _geometry.GridFromPoint(point);

            _tileHighlight.SetActive(true);
            _tileHighlight.transform.position = _geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                if (!_moveLocations.Contains(gridPoint))
                {
                    return;
                }

                if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    GameManager.instance.Move(_movingPiece, gridPoint);
                }
                else
                {
                    GameManager.instance.CapturePieceAt(gridPoint);
                    GameManager.instance.Move(_movingPiece, gridPoint);
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

        GameManager.instance.DeselectPiece(_movingPiece);
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }

    public void EnterState(GameObject piece)
    {
        _movingPiece = piece;
        enabled = true;

        _moveLocations = GameManager.instance.MovesForPiece(_movingPiece);
        _locationHighlights = new List<GameObject>();

        if (_moveLocations.Count == 0)
        {
            CancelMove();
        }

        foreach (Vector2Int loc in _moveLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(_attackLocationPrefab, _geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(_moveLocationPrefab, _geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            _locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        enabled = false;
        TileSelector selector = GetComponent<TileSelector>();
        _tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(_movingPiece);
        _movingPiece = null;
        GameManager.instance.NextPlayer();
        selector.EnterState();
        foreach (GameObject highlight in _locationHighlights)
        {
            Destroy(highlight);
        }
    }
}
