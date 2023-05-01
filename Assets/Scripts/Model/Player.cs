using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private List<GameObject> _pieces;
    private List<GameObject> _capturedPieces;
    private int _level;
    private int _expierence;

    public string Name { get; private set; }
    public int Forward { get; private set; }
    public int Level { get; private set; }
    public int Expierence { get; private set; }

    public Player(string name, bool positiveZMovement)
    {
        Name = name;
        Forward = positiveZMovement ? 1 : -1;
        _pieces = new List<GameObject>();
        _capturedPieces = new List<GameObject>();
        _level = 0;
        _expierence = 0;
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

    public void IncreaseLevel()
    {
        _level++;
    }

    public void IncreseExperience()
    {
        _expierence++;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}
