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
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = _pointConverter.PointFromGrid(gridPoint);
        Debug.Log(piece.transform.position.x + piece.transform.position.y + piece.transform.position.z);
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = _selectedMaterial;
        Debug.Log("matreial name of selected piece " + renderers.material.name);
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = _defaultMaterial;
    }
}
