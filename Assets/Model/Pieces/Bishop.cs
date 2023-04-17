using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        GameObject piecesCreator = GameObject.FindGameObjectWithTag("PiecesCreator");
        PiecesCreator _piecesCreator = piecesCreator.GetComponent<PiecesCreator>();
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int direction in BishopDirections)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * direction.x, gridPoint.y + i * direction.y);
                locations.Add(nextGridPoint);

                if (_piecesCreator.PieceAtGrid(nextGridPoint))
                    break;  
            }
        }
        return locations;
    }
}
