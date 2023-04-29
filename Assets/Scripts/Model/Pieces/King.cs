using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        List<Vector2Int> directions = new List<Vector2Int>(BishopDirections);
        directions.AddRange(RookDirections);

        foreach (Vector2Int direction in directions)
        {
            Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + direction.x, gridPoint.y + direction.y);
            locations.Add(nextGridPoint);
        }
        return locations;
    }
}
