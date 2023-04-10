using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private GameObject _tileHighlightPrefab;

    private GameObject _tileHighlight;
    private Geometry _geometry;

    void Start()
    {
        _geometry = GetComponent<Geometry>();
        Vector2Int gridPoint = _geometry.GridPoint(0, 0);
        Vector3 point = _geometry.PointFromGrid(gridPoint);
        _tileHighlight = Instantiate(_tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
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
                GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);
                if (GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece))
                {
                    GameManager.instance.SelectPiece(selectedPiece);
                    ExitState(selectedPiece);
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
