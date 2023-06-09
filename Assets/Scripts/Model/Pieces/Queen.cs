﻿using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        PieceTurnMover pieceTurnMover = GetPieceTurnMover();
        List<Vector2Int> locations = new List<Vector2Int>();
        List<Vector2Int> directions = new List<Vector2Int>(BishopDirections);
        directions.AddRange(RookDirections);

        foreach (Vector2Int direction in directions)
        {
            for (int i = 1; i < _maxMovementLenght; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * direction.x, gridPoint.y + i * direction.y);
                locations.Add(nextGridPoint);

                if (pieceTurnMover.PieceAtGrid(nextGridPoint) || pieceTurnMover.IsAbilityAt(nextGridPoint))
                    break;
            }
        }
        return locations;
    }
}
