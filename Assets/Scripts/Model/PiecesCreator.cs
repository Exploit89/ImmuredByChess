using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _whiteKing;
    [SerializeField] private GameObject _whiteQueen;
    [SerializeField] private GameObject _whiteBishop;
    [SerializeField] private GameObject _whiteKnight;
    [SerializeField] private GameObject _whiteRook;
    [SerializeField] private GameObject _whitePawn;
    [SerializeField] private GameObject _blackKing;
    [SerializeField] private GameObject _blackQueen;
    [SerializeField] private GameObject _blackBishop;
    [SerializeField] private GameObject _blackKnight;
    [SerializeField] private GameObject _blackRook;
    [SerializeField] private GameObject _blackPawn;

    private GameObject[,] _pieces;
    private PointConverter _pointConverter;
    private int _sideCount = 7;
    private int _maxPiecesCount = 16;

    void Start()
    {
        _pieces = new GameObject[_board.MaxSideLength, _board.MaxSideLength];
        _pointConverter = new PointConverter();
        InitialSetup();
    }

    private void InitialSetup()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("PlayerPieces");

        AddPiece(_whiteRook, _pieceTurnMover.Player, 0, 0, parent.transform);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 1, 0, parent.transform);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 2, 0, parent.transform);
        AddPiece(_whiteQueen, _pieceTurnMover.Player, 3, 0, parent.transform);
        AddPiece(_whiteKing, _pieceTurnMover.Player, 4, 0, parent.transform);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 5, 0, parent.transform);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 6, 0, parent.transform);
        AddPiece(_whiteRook, _pieceTurnMover.Player, 7, 0, parent.transform);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_whitePawn, _pieceTurnMover.Player, i, 1, parent.transform);
        }
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 0, 7, _board.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 1, 7, _board.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 2, 7, _board.transform);
        AddPiece(_blackQueen, _pieceTurnMover.Enemy, 3, 7, _board.transform);
        AddPiece(_blackKing, _pieceTurnMover.Enemy, 4, 7, _board.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 5, 7, _board.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 6, 7, _board.transform);
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 7, 7, _board.transform);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _pieceTurnMover.Enemy, i, 6, _board.transform);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int column, int row, Transform parent)
    {
        GameObject pieceObject = _board.AddPiece(prefab, column, row, parent);
        player.AddPiece(pieceObject);
        _pieces[column, row] = pieceObject;
    }

    public GameObject[,] GetPiecesList()
    {
        GameObject[,] pieces = new GameObject[,] { };
        pieces = _pieces;
        return pieces;
    }

    public void NewStageInitialSetup()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("PlayerPieces");
        Transform[] piecesTransform = parent.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i <= _sideCount; i++)
        {
            MovePieceToStartPosition(piecesTransform[i+1].gameObject, i, 0);
        }

        for (int i = 0; _board.MaxSideLength + i < _maxPiecesCount; i++)
        {
            MovePieceToStartPosition(piecesTransform[_board.MaxSideLength + i + 1].gameObject, i, 1);
        }

        AddPiece(_blackRook, _pieceTurnMover.Enemy, 0, 7, _board.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 1, 7, _board.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 2, 7, _board.transform);
        AddPiece(_blackQueen, _pieceTurnMover.Enemy, 3, 7, _board.transform);
        AddPiece(_blackKing, _pieceTurnMover.Enemy, 4, 7, _board.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 5, 7, _board.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 6, 7, _board.transform);
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 7, 7, _board.transform);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _pieceTurnMover.Enemy, i, 6, _board.transform);
        }
    }

    public void MovePieceToStartPosition(GameObject piece, int column, int row)
    {
        Vector2Int gridPoint = _pointConverter.GridPoint(column, row);
        _board.MovePiece(piece, gridPoint);
        _pieces[column, row] = piece;
        piece.SetActive(true);
    }
}
