using System.Collections.Generic;
using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
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
    private List<GameObject> _movedPawns;
    private Player _player;
    private Player _enemy;

    public Player CurrentPlayer { get; private set; }
    public Player OtherPlayer { get; private set; }

    void Start()
    {
        _pieces = new GameObject[_board.MaxSideLength, _board.MaxSideLength];
        _movedPawns = new List<GameObject>();
        _player = new Player("_player", true);
        _enemy = new Player("_enemy", false);
        CurrentPlayer = _player;
        OtherPlayer = _enemy;
        InitialSetup();
    }

    private void InitialSetup()
    {
        AddPiece(_whiteRook, _player, 0, 0);
        AddPiece(_whiteKnight, _player, 1, 0);
        AddPiece(_whiteBishop, _player, 2, 0);
        AddPiece(_whiteQueen, _player, 3, 0);
        AddPiece(_whiteKing, _player, 4, 0);
        AddPiece(_whiteBishop, _player, 5, 0);
        AddPiece(_whiteKnight, _player, 6, 0);
        AddPiece(_whiteRook, _player, 7, 0);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_whitePawn, _player, i, 1);
        }
        AddPiece(_blackRook, _enemy, 0, 7);
        AddPiece(_blackKnight, _enemy, 1, 7);
        AddPiece(_blackBishop, _enemy, 2, 7);
        AddPiece(_blackQueen, _enemy, 3, 7);
        AddPiece(_blackKing, _enemy, 4, 7);
        AddPiece(_blackBishop, _enemy, 5, 7);
        AddPiece(_blackKnight, _enemy, 6, 7);
        AddPiece(_blackRook, _enemy, 7, 7);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _enemy, i, 6);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int column, int row)
    {
        GameObject pieceObject = _board.AddPiece(prefab, column, row);
        player.AddPiece(pieceObject);
        _pieces[column, row] = pieceObject;
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = _pieces[gridPoint.x, gridPoint.y];

        if (selectedPiece)
            _board.SelectPiece(selectedPiece);
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);
        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);
        locations.RemoveAll(gp => FriendlyPieceAt(gp));
        return locations;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();

        if (pieceComponent.Type == PieceType.Pawn && !HasPawnMoved(piece))
        {
            _movedPawns.Add(piece);
        }

        Vector2Int startGridPoint = GridForPiece(piece);
        _pieces[startGridPoint.x, startGridPoint.y] = null;
        _pieces[gridPoint.x, gridPoint.y] = piece;
        _board.MovePiece(piece, gridPoint);
    }

    public bool HasPawnMoved(GameObject pawn)
    {
        return _movedPawns.Contains(pawn);
    }

    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);

        if (pieceToCapture.GetComponent<Piece>().Type == PieceType.King)
        {
            Destroy(_board.GetComponent<TileSelector>());
            Destroy(_board.GetComponent<MoveSelector>());
        }
        CurrentPlayer.AddCapturedPiece(pieceToCapture);
        _pieces[gridPoint.x, gridPoint.y] = null;
        Destroy(pieceToCapture);
    }

    public void SelectPiece(GameObject piece)
    {
        _board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        _board.DeselectPiece(piece);
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return CurrentPlayer.ContainsPiece(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
            return null;
        return _pieces[gridPoint.x, gridPoint.y];
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                if (_pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null)
            return false;

        return OtherPlayer.ContainsPiece(piece) ? false : true;
    }

    public void NextPlayer()
    {
        Player tempPlayer = CurrentPlayer;
        CurrentPlayer = OtherPlayer;
        OtherPlayer = tempPlayer;
    }
}
