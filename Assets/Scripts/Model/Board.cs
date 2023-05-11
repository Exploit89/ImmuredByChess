using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _whiteMaterial;
    [SerializeField] private Material _blackMaterial;
    [SerializeField] private Material _selectedMaterial;

    private PointConverter _pointConverter;
    private int _modelRotateAngleY = 90;
    private List<GameObject> _piecesOnBoard = new List<GameObject>();

    public int MaxSideLength { get; private set; } = 8;

    private void Awake()
    {
        _pointConverter = new PointConverter();
    }

    public GameObject AddPiece(GameObject piece, int column, int row, Transform parent)
    {
        Quaternion knightRotation = Quaternion.Euler(0, -_modelRotateAngleY, 0);
        Vector2Int gridPoint = _pointConverter.GridPoint(column, row);

        if (piece.name == "White_Knight" || piece.name == "White_Knight(Clone)")
        {
            GameObject newPiece = Instantiate(piece, _pointConverter.PointFromGrid(gridPoint), knightRotation, parent);

            if (newPiece.TryGetComponent(out Unit unit) == false)
                newPiece.AddComponent<Unit>();
            newPiece.name = piece.name;
            _piecesOnBoard.Add(newPiece);
            return newPiece;
        }
        else
        {
            GameObject newPiece = Instantiate(piece, _pointConverter.PointFromGrid(gridPoint), Quaternion.Euler(0, _modelRotateAngleY, 0), parent);

            if(newPiece.TryGetComponent(out Unit unit) == false)
                newPiece.AddComponent<Unit>();
            newPiece.name = piece.name;
            _piecesOnBoard.Add(newPiece);
            return newPiece;
        }
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
            renderers.material = _whiteMaterial;
        else
            renderers.material = _blackMaterial;
    }

    public void ClearBoard()
    {
        foreach(var piece in _piecesOnBoard)
        {
            piece.SetActive(false);
        }
        //_piecesOnBoard.Clear();
    }
}
