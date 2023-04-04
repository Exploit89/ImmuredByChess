using System;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private enum Figure
    {
        LeftRook,
        LeftKnight,
        LeftBishop,
        Queen,
        King,
        RightBishop,
        RightKnight,
        RightRook,
        Pawn1,
        Pawn2,
        Pawn3,
        Pawn4,
        Pawn5,
        Pawn6,
        Pawn7,
        Pawn8
    }

    private Dictionary<String, Vector3> _playerFiguresPoints = new Dictionary<string, Vector3>();
    private Dictionary<String, Vector3> _enemyFiguresPoints = new Dictionary<string, Vector3>();

    public int MaxFiguresRows { get; private set; } = 2;
    public int MaxFiguresColumns { get; private set; } = 8;

    public void CreateFiguresPoints(Vector3 playerStartPosition, Vector3 enemyStartPosition)
    {
        _playerFiguresPoints.Clear();
        _enemyFiguresPoints.Clear();

        for (int i = 0; i < MaxFiguresColumns * MaxFiguresRows; i++)
        {
            if(i < MaxFiguresColumns)
            {
                _playerFiguresPoints.Add(Enum.GetName(typeof(Figure), i), new Vector3(playerStartPosition.x + i, playerStartPosition.y));
                _enemyFiguresPoints.Add(Enum.GetName(typeof(Figure), i), new Vector3(enemyStartPosition.x + i, enemyStartPosition.y));
            }
            else
            {
                _playerFiguresPoints.Add(Enum.GetName(typeof(Figure), i), new Vector3(playerStartPosition.x - MaxFiguresColumns + i, playerStartPosition.y + 1));
                _enemyFiguresPoints.Add(Enum.GetName(typeof(Figure), i), new Vector3(enemyStartPosition.x - MaxFiguresColumns + i, enemyStartPosition.y - 1));
            }
        }
    }

    public Dictionary<String, Vector3> GetPlayerFiguresPoints()
    {
        Dictionary<String, Vector3> playerFiguresPoints = new Dictionary<String, Vector3>();
        playerFiguresPoints = _playerFiguresPoints;
        return playerFiguresPoints;
    }
}
