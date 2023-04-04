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

    private int[] _figuresFormationKeys;

    private Dictionary<Figure, Vector3> _playerFiguresPoints;
    private Dictionary<Figure, Vector3> _enemyFiguresPoints;

    public int MaxFiguresRows { get; private set; }
    public int MaxFiguresColumns { get; private set; }

    private void Awake()
    {
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
