using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceTurnMover : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PiecesCreator _piecesCreator;
    [SerializeField] private ExperienceCalculator _experienceCalculator;
    [SerializeField] private Rewarder _rewarder;
    [SerializeField] private MoveSelector _moveSelector;
    [SerializeField] private BonusCreator _bonusCreator;

    private List<GameObject> _movedPawns;
    private bool _isKingDestroyed = false;

    public bool IsSetupRestarted { get; private set; } = false;
    public Player Player { get; private set; }
    public Player Enemy { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public Player OtherPlayer { get; private set; }
    public GameObject CurrentPiece { get; private set; }
    public int TurnCount { get; private set; } = 1;

    public event UnityAction MatchEnded;
    public event UnityAction ExperienceIncreased;
    public event UnityAction LevelIncreased;
    public event UnityAction GameLevelCompleted;
    public event UnityAction GameLevelLost;
    public event UnityAction TurnChanged;

    private void Awake()
    {
        Player = new Player("_player", true);
        Enemy = new Player("_enemy", false);
        CurrentPlayer = Player;
        OtherPlayer = Enemy;
    }

    private void Start()
    {
        _movedPawns = new List<GameObject>();
    }

    private Unit GetCurrentPiece()
    {
        TileSelector tileSelector = _board.GetComponent<TileSelector>();
        GameObject selectedPiece = tileSelector.GetSelectedPiece();
        Unit selectedUnit = selectedPiece.GetComponent<Unit>();
        return selectedUnit;
    }

    private void GetReward(GameObject pieceToCapture)
    {
        PieceType pieceToCaptureType = pieceToCapture.GetComponent<Piece>().Type;
        Rank pieceToCaptureRank = pieceToCapture.GetComponent<Unit>().UnitRank;
        int experienceReward = _experienceCalculator.GetExperienceReward(pieceToCaptureType);
        TileSelector tileSelector = _board.GetComponent<TileSelector>();
        GameObject selectedPiece = tileSelector.GetSelectedPiece();
        CurrentPlayer.IncreaseExperience(experienceReward);
        CurrentPlayer.AddMoney(_rewarder.GetMoneyReward(pieceToCaptureType, pieceToCaptureRank));

        if (CurrentPlayer == Player)
            Player.AddExperienceToPiece(selectedPiece, experienceReward);
    }

    private bool IsKingDestroyed(GameObject pieceToCapture)
    {
        bool destroyed = false;

        if (pieceToCapture.GetComponent<Piece>().Type == PieceType.King)
            destroyed = true;
        return destroyed;
    }

    public bool IsTargetDead(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        float targetHealth = pieceToCapture.GetComponent<Unit>().Health;

        if (targetHealth <= 0)
            return true;
        return false;
    }

    public void TryDestroyTarget(Vector2Int gridPoint)
    {
        if (IsTargetDead(gridPoint))
            CapturePieceAt(gridPoint);
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);
        locations.RemoveAll(gridPointBoard => gridPointBoard.x < 0 || gridPointBoard.x > 7 || gridPointBoard.y < 0 || gridPointBoard.y > 7);
        locations.RemoveAll(gridPointBoard => IsFriendlyPieceAt(gridPointBoard));
        locations.RemoveAll(gridPointBoard => _bonusCreator.IsAbilityAt(gridPointBoard));
        return locations;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();

        if (pieceComponent.Type == PieceType.Pawn && !IsPawnMoved(piece))
            _movedPawns.Add(piece);
        Vector2Int startGridPoint = GridForPiece(piece);
        _piecesCreator.GetPiecesList()[startGridPoint.x, startGridPoint.y] = null;
        _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y] = piece;
        _board.MovePiece(piece, gridPoint);
    }

    public void TryMove(GameObject piece, Vector2Int gridPoint)
    {
        if (IsSetupRestarted)
            return;
        GameObject pieceToCapture = PieceAtGrid(gridPoint);

        if (pieceToCapture == null)
            Move(piece, gridPoint);
    }

    public bool IsPawnMoved(GameObject pawn)
    {
        return _movedPawns.Contains(pawn);
    }

    public void ClearMovedPawns()
    {
        _movedPawns.Clear();
    }

    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y] = null;
        CurrentPlayer.AddCapturedPiece(pieceToCapture);
        GetReward(pieceToCapture); // TODO здесь поместить вызов view для отображения полученных монет
        ExperienceIncreased?.Invoke();

        if (_experienceCalculator.IsPlayerLevelReached(CurrentPlayer.Experience, CurrentPlayer.Level))
        {
            CurrentPlayer.IncreaseLevel();
            LevelIncreased?.Invoke();
        }

        if (_experienceCalculator.IsUnitLevelReached(GetCurrentPiece().Experience, GetCurrentPiece().Level))
        {
            GetCurrentPiece().IncreaseLevel();
            Debug.Log("level icreased " + GetCurrentPiece().Level);
        }

        if (pieceToCapture.GetComponent<Piece>().Type == PieceType.King)
        {
            MatchEnded?.Invoke();
            _isKingDestroyed = IsKingDestroyed(pieceToCapture);
        }
        pieceToCapture.SetActive(false); // TODO здесь поместить вызов view для отображения смерти

        if (_isKingDestroyed)
        {
            TurnCount = 1;
            _moveSelector.CancelState();
            _board.ClearBoard();

            if (CurrentPlayer == Player)
                GameLevelCompleted?.Invoke();
            else GameLevelLost?.Invoke();
            _piecesCreator.NewStageInitialSetup();
            IsSetupRestarted = true;
            _isKingDestroyed = false;
        }
    }

    public void SelectPiece(GameObject piece)
    {
        _board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        _board.DeselectPiece(piece);
    }

    public bool IsPieceBelongToCurrentPlayer(GameObject piece)
    {
        return CurrentPlayer.ContainsPiece(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
            return null;
        return _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y];
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                if (_piecesCreator.GetPiecesList()[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public bool IsFriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null)
            return false;
        return OtherPlayer.ContainsPiece(piece) ? false : true;
    }

    public bool IsAbilityAt(Vector2Int gridPoint)
    {
        return _bonusCreator.IsAbilityAt(gridPoint);
    }

    public void NextPlayer()
    {
        Player tempPlayer = CurrentPlayer;
        CurrentPlayer = OtherPlayer;
        OtherPlayer = tempPlayer;
        TurnCount++;
        TurnChanged?.Invoke();
    }

    public void TurnOffSetupRestarted()
    {
        IsSetupRestarted = false;
    }
}
