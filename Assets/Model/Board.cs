using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _whiteMaterial;
    [SerializeField] private Material _blackMaterial;
    [SerializeField] private Material _selectedMaterial;

    private PointConverter _pointConverter;

    private void Awake()
    {
        _pointConverter = new PointConverter();
    }

    public GameObject AddPiece(GameObject piece, int column, int row)
    {
        Quaternion knightRotation = Quaternion.Euler(0, -90, 0);
        Vector2Int gridPoint = _pointConverter.GridPoint(column, row);

        if (piece.name == "White_Knight")
        {
            GameObject newPiece = Instantiate(piece, _pointConverter.PointFromGrid(gridPoint), knightRotation, gameObject.transform);
            return newPiece;
        }
        else
        {
            GameObject newPiece = Instantiate(piece, _pointConverter.PointFromGrid(gridPoint), Quaternion.Euler(0, 90, 0), gameObject.transform);
            return newPiece;
        }
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = _pointConverter.PointFromGrid(gridPoint);
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = _selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();

        if(piece.tag == "White")
        {
            renderers.material = _whiteMaterial;
        }
        else
        {
            renderers.material = _blackMaterial;
        }
    }
}
