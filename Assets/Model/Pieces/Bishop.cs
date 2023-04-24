using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        GameObject gameplayRuler = GameObject.FindGameObjectWithTag("GameplayRuler");
        GameplayRuler _gameplayRuler = gameplayRuler.GetComponent<GameplayRuler>();
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int direction in BishopDirections)
        {
            for (int i = 1; i < _maxMovementLenght; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * direction.x, gridPoint.y + i * direction.y);
                locations.Add(nextGridPoint);

                if (_gameplayRuler.PieceAtGrid(nextGridPoint))
                    break;  
            }
        }
        return locations;
    }
}
