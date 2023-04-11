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
        Debug.Log("added piece to player" + pieceObject.name);
    }

    public void AddCapturedPiece(GameObject pieceToCapture)
    {
        _capturedPieces.Add(pieceToCapture);
        Debug.Log("added captured piece to player" + pieceToCapture.name);
    }

    public bool ContainsPiece(GameObject piece)
    {
        Debug.Log("contains piece" + piece);
        return _pieces.Contains(piece);
    }
}
