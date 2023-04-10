using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;

    private PointConverter _pointConverter;

    private void Awake()
    {
        _pointConverter = new PointConverter();
    }

    public GameObject AddPiece(GameObject piece, int column, int row)
    {
        Vector2Int gridPoint = _pointConverter.GridPoint(column, row);
        GameObject newPiece = Instantiate(piece, _pointConverter.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
        Debug.Log("instantiate" + newPiece.name);
        return newPiece;
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
        renderers.material = _defaultMaterial;
    }
}
