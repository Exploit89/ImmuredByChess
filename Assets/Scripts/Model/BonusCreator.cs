using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
    [SerializeField] private PiecesCreator _piecesCreator;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private Board _board;
    [SerializeField] private GameObject _healBonusPrefab;

    private PointConverter _pointConverter;
    private GameObject[,] _pieces;
    private List<Vector2Int> _cleanTiles;
    private List<Vector2Int> _occupiedTiles;
    private System.Random _random;
    private GameObject _healBonus;
    private int _turnCount;
    private int _turnCountToNextBonus = 5;

    private void Awake()
    {
        _random = new System.Random();
        _pieces = new GameObject[,] { };
        _cleanTiles = new List<Vector2Int>();
        _occupiedTiles = new List<Vector2Int>();
        _pointConverter = new PointConverter();
    }

    private void Start()
    {
        _healBonus = Instantiate(_healBonusPrefab);
        _healBonus.SetActive(false);
    }

    private void OnEnable()
    {
        _pieceTurnMover.TurnChanged += CountToNextBonus;
    }

    private void OnDisable()
    {
        _pieceTurnMover.TurnChanged -= CountToNextBonus;
    }

    private void CreateBonus()
    {
        ClearTiles();
        GetCleanTiles();
        int random = _random.Next(_cleanTiles.Count);
        Vector2Int vector2 = _pointConverter.GridPoint(_cleanTiles[random].x, _cleanTiles[random].y);
        Vector3 vector3 = _pointConverter.PointFromGrid(vector2);
        vector3 += new Vector3(0, 0.5f, 0);
        _healBonus.transform.position = vector3;
        _healBonus.GetComponent<HealBonus>().CreateEffect();
        _healBonus.GetComponent<HealBonus>().SetEffectPosition(vector3);
        _healBonus.SetActive(true);
    }

    private void GetCleanTiles()
    {
        _pieces = _piecesCreator.GetPiecesList();

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                if (_pieces[i,j] != null)
                {
                    _occupiedTiles.Add(_pointConverter.GridPoint(i, j));
                }
            }
        }

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                Vector2Int vector = _pointConverter.GridPoint(i, j);

                if(!_occupiedTiles.Contains(vector))
                    _cleanTiles.Add(vector);
            }
        }
    }

    private void CountToNextBonus()
    {
        _turnCount++;

        if (_turnCount == _turnCountToNextBonus)
        {
            CreateBonus();
            _turnCount = 0;
        }
    }

    private void ClearTiles()
    {
        _cleanTiles.Clear();
    }
}
