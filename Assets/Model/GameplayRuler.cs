using System.Collections.Generic;
using UnityEngine;

public class GameplayRuler : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PiecesCreator _piecesCreator;

    private List<GameObject> _movedPawns;

    public Player Player { get; private set; }
    public Player Enemy { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public Player OtherPlayer { get; private set; }


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
    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y];

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
        _piecesCreator.GetPiecesList()[startGridPoint.x, startGridPoint.y] = null;
        _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y] = piece;
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
        _piecesCreator.GetPiecesList()[gridPoint.x, gridPoint.y] = null;
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
