using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        GameObject gameplayRuler = GameObject.FindGameObjectWithTag("GameplayRuler");
        GameplayRuler _gameplayRuler = gameplayRuler.GetComponent<GameplayRuler>();
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = _gameplayRuler.CurrentPlayer.Forward;
        Vector2Int forwardOne = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);

        if (_gameplayRuler.PieceAtGrid(forwardOne) == false)
            locations.Add(forwardOne);
        Vector2Int forwardDouble = new Vector2Int(gridPoint.x, gridPoint.y + 2 * forwardDirection);

        if (_gameplayRuler.HasPawnMoved(gameObject) == false && _gameplayRuler.PieceAtGrid(forwardDouble) == false)
            locations.Add(forwardDouble);
        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);

        if (_gameplayRuler.PieceAtGrid(forwardRight))
            locations.Add(forwardRight);
        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);

        if (_gameplayRuler.PieceAtGrid(forwardLeft))
            locations.Add(forwardLeft);
        return locations;
    }
}
