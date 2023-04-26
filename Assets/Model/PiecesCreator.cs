using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PieceTurnMover _gameplayRuler;
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

    void Start()
    {
        _pieces = new GameObject[_board.MaxSideLength, _board.MaxSideLength];
        InitialSetup();
    }

    private void InitialSetup()
    {
        AddPiece(_whiteRook, _gameplayRuler.Player, 0, 0);
        AddPiece(_whiteKnight, _gameplayRuler.Player, 1, 0);
        AddPiece(_whiteBishop, _gameplayRuler.Player, 2, 0);
        AddPiece(_whiteQueen, _gameplayRuler.Player, 3, 0);
        AddPiece(_whiteKing, _gameplayRuler.Player, 4, 0);
        AddPiece(_whiteBishop, _gameplayRuler.Player, 5, 0);
        AddPiece(_whiteKnight, _gameplayRuler.Player, 6, 0);
        AddPiece(_whiteRook, _gameplayRuler.Player, 7, 0);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_whitePawn, _gameplayRuler.Player, i, 1);
        }
        AddPiece(_blackRook, _gameplayRuler.Enemy, 0, 7);
        AddPiece(_blackKnight, _gameplayRuler.Enemy, 1, 7);
        AddPiece(_blackBishop, _gameplayRuler.Enemy, 2, 7);
        AddPiece(_blackQueen, _gameplayRuler.Enemy, 3, 7);
        AddPiece(_blackKing, _gameplayRuler.Enemy, 4, 7);
        AddPiece(_blackBishop, _gameplayRuler.Enemy, 5, 7);
        AddPiece(_blackKnight, _gameplayRuler.Enemy, 6, 7);
        AddPiece(_blackRook, _gameplayRuler.Enemy, 7, 7);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _gameplayRuler.Enemy, i, 6);
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
}
