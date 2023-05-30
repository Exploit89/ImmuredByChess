﻿using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        GameObject gameplayRuler = GameObject.FindGameObjectWithTag("PieceTurnMover");
        PieceTurnMover _gameplayRuler = gameplayRuler.GetComponent<PieceTurnMover>();
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int direction in RookDirections)
        {
            for (int i = 1; i < _maxMovementLenght; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * direction.x, gridPoint.y + i * direction.y);
                locations.Add(nextGridPoint);

                if (_gameplayRuler.PieceAtGrid(nextGridPoint) || _gameplayRuler.IsAbilityAt(nextGridPoint))
                    break;
            }
        }
        return locations;
    }
}
