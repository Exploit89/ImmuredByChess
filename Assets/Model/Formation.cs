using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private enum Figure
    {
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        Pawn
    }

    private int[] _playerFiguresFormationKeys;
    private int[] _enemyFiguresFormationKeys;

    private Dictionary<Figure, Vector3> _playerFiguresPoints;
    private Dictionary<Figure, Vector3> _enemyFiguresPoints;

    public int MaxFiguresRows { get; private set; }
    public int MaxFiguresColumns { get; private set; }

    private void Awake()
    {
        _playerFiguresFormationKeys = new int[] { 0, 1, 2, 3, 4, 2, 1, 0, 5, 5, 5, 5, 5, 5, 5, 5 };
        _enemyFiguresFormationKeys = new int[] { 0, 1, 2, 3, 4, 2, 1, 0, 5, 5, 5, 5, 5, 5, 5, 5 };
        MaxFiguresRows = 2;
        MaxFiguresColumns = 8;
        _playerFiguresPoints = new Dictionary<Figure, Vector3>();
        _enemyFiguresPoints = new Dictionary<Figure, Vector3>();
    }

    public void CreatePlayerFiguresPoints(Vector3 startPosition)
    {
        _playerFiguresPoints.Clear();
        _playerFiguresPoints.Add(0, startPosition);

    }

    public void CreateEnemyFiguresPoints(Vector3 startPosition)
    {
        _enemyFiguresPoints.Clear();
    }
}
