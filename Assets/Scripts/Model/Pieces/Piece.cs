﻿using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    King,
    Queen,
    Bishop,
    Knight,
    Rook,
    Pawn
};

public abstract class Piece : MonoBehaviour
{
    [SerializeField] private protected PieceType _type;

    protected int _maxMovementLenght = 8;
    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0),
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};
    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1),
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    protected PieceTurnMover GetPieceTurnMover()
    {
        GameObject piece = GetComponentInParent<Transform>().gameObject;
        GameObject playerPieces = piece.GetComponentInParent<PiecesGroup>().gameObject;
        GameObject board = playerPieces.GetComponentInParent<Board>().gameObject;
        return board.GetComponentInChildren<PieceTurnMover>();
    }

    public PieceType Type => _type;

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);
}
