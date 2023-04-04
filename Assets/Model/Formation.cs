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
    private Dictionary<String, Vector3> _playerFiguresPoints;
    private Dictionary<Figure, Vector3> _enemyFiguresPoints;

    public int MaxFiguresRows { get; private set; }
    public int MaxFiguresColumns { get; private set; }

    private void Awake()
    {
        _figuresFormationKeys = new int[] { 0, 1, 2, 3, 4, 2, 1, 0, 5, 5, 5, 5, 5, 5, 5, 5 };
        MaxFiguresRows = 2;
        MaxFiguresColumns = 8;
        _playerFiguresPoints = new Dictionary<String, Vector3>();
        _enemyFiguresPoints = new Dictionary<Figure, Vector3>();

        CreatePlayerFiguresPoints(new Vector3(0,0));
    }

    public void CreatePlayerFiguresPoints(Vector3 startPosition)
    {
        _playerFiguresPoints.Clear();

        for (int i = 0; i < _figuresFormationKeys.Length; i++)
        {
            _playerFiguresPoints.Add(Enum.GetName(typeof(Figure), _figuresFormationKeys[i]), new Vector3(startPosition.x + i, startPosition.y));
            Debug.Log(_playerFiguresPoints[Enum.GetName(typeof(Figure), _figuresFormationKeys[i])]);
        }
    }

    public void CreateEnemyFiguresPoints(Vector3 startPosition)
    {
        _enemyFiguresPoints.Clear();
    }
}
