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
    private int _sideCount = 7;

    void Start()
    {
        _pieces = new GameObject[_board.MaxSideLength, _board.MaxSideLength];
        InitialSetup();
    }

    private void InitialSetup()
    {
        AddPiece(_whiteRook, _pieceTurnMover.Player, 0, 0);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 1, 0);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 2, 0);
        AddPiece(_whiteQueen, _pieceTurnMover.Player, 3, 0);
        AddPiece(_whiteKing, _pieceTurnMover.Player, 4, 0);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 5, 0);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 6, 0);
        AddPiece(_whiteRook, _pieceTurnMover.Player, 7, 0);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_whitePawn, _pieceTurnMover.Player, i, 1);
        }
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 0, 7);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 1, 7);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 2, 7);
        AddPiece(_blackQueen, _pieceTurnMover.Enemy, 3, 7);
        AddPiece(_blackKing, _pieceTurnMover.Enemy, 4, 7);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 5, 7);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 6, 7);
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 7, 7);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _pieceTurnMover.Enemy, i, 6);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int column, int row)
    {
        GameObject pieceObject = _board.AddPiece(prefab, column, row);
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
        for (int i = 0; i <= _sideCount; i++)
        {
            AddExistPiece(_pieceTurnMover.Player.GetPieces()[i], i, 0);
        }

        for (int i = 0; _board.MaxSideLength + i < _pieceTurnMover.Player.GetPieces().Count; i++)
        {
            AddExistPiece(_pieceTurnMover.Player.GetPieces()[_board.MaxSideLength + i], i, 1);
        }

        AddPiece(_blackRook, _pieceTurnMover.Enemy, 0, 7);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 1, 7);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 2, 7);
        AddPiece(_blackQueen, _pieceTurnMover.Enemy, 3, 7);
        AddPiece(_blackKing, _pieceTurnMover.Enemy, 4, 7);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 5, 7);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 6, 7);
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 7, 7);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _pieceTurnMover.Enemy, i, 6);
        }
    }

    public void AddExistPiece(GameObject piece, int column, int row)
    {
        GameObject pieceObject = _board.AddPiece(piece, column, row);
        _pieces[column, row] = pieceObject;
    }
}
