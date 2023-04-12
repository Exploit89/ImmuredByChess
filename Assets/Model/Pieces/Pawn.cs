using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private PiecesCreator piecesCreator;

    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        piecesCreator = GetComponent<PiecesCreator>();
        List<Vector2Int> locations = new List<Vector2Int>();

        int forwardDirection = piecesCreator.currentPlayer.Forward;
        Debug.Log("Pawn forwardDirection " + forwardDirection);
        Vector2Int forwardOne = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        if (piecesCreator.PieceAtGrid(forwardOne) == false)
        {
            locations.Add(forwardOne);
        }

        Vector2Int forwardDouble = new Vector2Int(gridPoint.x, gridPoint.y + 2 * forwardDirection);
        if (piecesCreator.HasPawnMoved(gameObject) == false && piecesCreator.PieceAtGrid(forwardDouble) == false)
        {
            locations.Add(forwardDouble);
        }

        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
        if (piecesCreator.PieceAtGrid(forwardRight))
        {
            locations.Add(forwardRight);
        }

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);
        if (piecesCreator.PieceAtGrid(forwardLeft))
        {
            locations.Add(forwardLeft);
        }

        return locations;
    }
}
