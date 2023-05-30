using UnityEngine;
using UnityEngine.Events;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private HitPointBarCreator _hitPointBarCreator;
    [SerializeField] private GameLevel _gameLevel;
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

    public event UnityAction PieceMoved;

    private void Start()
    {
        _pieces = new GameObject[_board.MaxSideLength, _board.MaxSideLength];
        _pointConverter = new PointConverter();
        InitialSetup();
        _hitPointBarCreator.CreateHitPointBars();
        PieceMoved?.Invoke();
    }

    private void InitialEnemySetup()
    {
        GameObject parentEnemy = GameObject.FindGameObjectWithTag("EnemyPieces");
        Transform[] piecesEnemyTransform = parentEnemy.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i <= _sideCount; i++)
        {
            GameObject piece = piecesEnemyTransform[i + 1].gameObject;
            PieceType pieceType = piece.GetComponent<Piece>().Type;
            GameLevelSetup gameLevelSetup = _gameLevel.CreateEnemySetup(pieceType);
            piece.GetComponent<Unit>().LoadUnitSetup(gameLevelSetup);
            MovePieceToStartPosition(piece, i, 7);
        }

        for (int i = 0; _board.MaxSideLength + i < _maxPiecesCount; i++)
        {
            GameObject piece = piecesEnemyTransform[_board.MaxSideLength + i + 1].gameObject;
            PieceType pieceType = piece.GetComponent<Piece>().Type;
            GameLevelSetup gameLevelSetup = _gameLevel.CreateEnemySetup(pieceType);
            piece.GetComponent<Unit>().LoadUnitSetup(gameLevelSetup);
            MovePieceToStartPosition(piece, i, 6);
        }
    }

    private void InitialSetup()
    {
        GameObject parentPlayer = GameObject.FindGameObjectWithTag("PlayerPieces");
        GameObject parentEnemy = GameObject.FindGameObjectWithTag("EnemyPieces");

        AddPiece(_whiteRook, _pieceTurnMover.Player, 0, 0, parentPlayer.transform);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 1, 0, parentPlayer.transform);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 2, 0, parentPlayer.transform);
        AddPiece(_whiteQueen, _pieceTurnMover.Player, 3, 0, parentPlayer.transform);
        AddPiece(_whiteKing, _pieceTurnMover.Player, 4, 0, parentPlayer.transform);
        AddPiece(_whiteBishop, _pieceTurnMover.Player, 5, 0, parentPlayer.transform);
        AddPiece(_whiteKnight, _pieceTurnMover.Player, 6, 0, parentPlayer.transform);
        AddPiece(_whiteRook, _pieceTurnMover.Player, 7, 0, parentPlayer.transform);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_whitePawn, _pieceTurnMover.Player, i, 1, parentPlayer.transform);
        }
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 0, 7, parentEnemy.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 1, 7, parentEnemy.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 2, 7, parentEnemy.transform);
        AddPiece(_blackQueen, _pieceTurnMover.Enemy, 3, 7, parentEnemy.transform);
        AddPiece(_blackKing, _pieceTurnMover.Enemy, 4, 7, parentEnemy.transform);
        AddPiece(_blackBishop, _pieceTurnMover.Enemy, 5, 7, parentEnemy.transform);
        AddPiece(_blackKnight, _pieceTurnMover.Enemy, 6, 7, parentEnemy.transform);
        AddPiece(_blackRook, _pieceTurnMover.Enemy, 7, 7, parentEnemy.transform);

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            AddPiece(_blackPawn, _pieceTurnMover.Enemy, i, 6, parentEnemy.transform);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int column, int row, Transform parent)
    {
        GameObject pieceObject = _board.AddPiece(prefab, column, row, parent);
        player.AddPiece(pieceObject);
        pieceObject.GetComponent<Unit>().AddBaseSkill();
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
        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                _pieces[i, j] = null;
            }
        }

        _pieceTurnMover.ClearMovedPawns();
        GameObject parentPlayer = GameObject.FindGameObjectWithTag("PlayerPieces");
        Transform[] piecesTransform = parentPlayer.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i <= _sideCount; i++)
        {
            GameObject piece = piecesTransform[i + 1].gameObject;
            MovePieceToStartPosition(piece, i, 0);
        }

        for (int i = 0; _board.MaxSideLength + i < _maxPiecesCount; i++)
        {
            GameObject piece = piecesTransform[_board.MaxSideLength + i + 1].gameObject;
            MovePieceToStartPosition(piece, i, 1);
        }
        InitialEnemySetup();
        PieceMoved?.Invoke();
    }

    public void MovePieceToStartPosition(GameObject piece, int column, int row)
    {
        Vector2Int gridPoint = _pointConverter.GridPoint(column, row);
        _board.MovePiece(piece, gridPoint);
        _pieces[column, row] = piece;
        piece.SetActive(true);
    }
}
