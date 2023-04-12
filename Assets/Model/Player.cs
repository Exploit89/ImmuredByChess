using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private List<GameObject> _pieces;
    private List<GameObject> _capturedPieces;

    public string Name { get; private set; }
    public int Forward { get; private set; }

    public Player(string name, bool positiveZMovement)
    {
        Name = name;
        _pieces = new List<GameObject>();
        _capturedPieces = new List<GameObject>();

        if (positiveZMovement == true)
        {
            Forward = 1;
        }
        else
        {
            Forward = -1;
        }
    }

    public void AddPiece(GameObject pieceObject)
    {
        _pieces.Add(pieceObject);
    }

    public void AddCapturedPiece(GameObject pieceToCapture)
    {
        _capturedPieces.Add(pieceToCapture);
    }

    public bool ContainsPiece(GameObject piece)
    {
        return _pieces.Contains(piece);
    }
}
