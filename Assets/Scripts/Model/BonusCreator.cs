using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
    [SerializeField] private PiecesCreator _piecesCreator;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private Board _board;
    [SerializeField] private GameObject _healBonusPrefab;
    [SerializeField] private GameObject _wallBonusPrefab;

    private PointConverter _pointConverter;
    private GameObject[,] _pieces;
    private List<Vector2Int> _cleanTiles;
    private List<Vector2Int> _occupiedTiles;
    private List<Vector2Int> _occupiedByBonus;
    private List<GameObject> _bonuses;
    private System.Random _random;
    private GameObject _healBonus;
    private GameObject _wallBonus;
    private int _turnCount;
    private int _turnCountToNextBonus = 5;

    private void Awake()
    {
        _random = new System.Random();
        _pieces = new GameObject[,] { };
        _cleanTiles = new List<Vector2Int>();
        _occupiedTiles = new List<Vector2Int>();
        _occupiedByBonus = new List<Vector2Int>();
        _bonuses = new List<GameObject>();
        _pointConverter = new PointConverter();
    }

    private void Start()
    {
        _healBonus = Instantiate(_healBonusPrefab);
        _healBonus.SetActive(false);
        _healBonus.GetComponent<Effect>().CreateEffect();
        _wallBonus = Instantiate(_wallBonusPrefab);
        _wallBonus.SetActive(false);
        _wallBonus.GetComponent<Effect>().CreateEffect();
        _bonuses.Add(_healBonus);
        _bonuses.Add(_wallBonus);
    }

    private void OnEnable()
    {
        _pieceTurnMover.TurnChanged += CountToNextBonus;
        _pieceTurnMover.GameLevelCompleted += ClearAllOccupiedTiles;
        _pieceTurnMover.GameLevelLost += ClearAllOccupiedTiles;
    }

    private void OnDisable()
    {
        _pieceTurnMover.TurnChanged -= CountToNextBonus;
        _pieceTurnMover.GameLevelCompleted -= ClearAllOccupiedTiles;
        _pieceTurnMover.GameLevelLost -= ClearAllOccupiedTiles;
    }

    private void CreateBonus()
    {
        ClearTiles();
        GetCleanTiles();
        int random = _random.Next(_cleanTiles.Count);
        Vector2Int vector2 = _pointConverter.GridPoint(_cleanTiles[random].x, _cleanTiles[random].y);
        Vector3 vector3 = _pointConverter.PointFromGrid(vector2);
        vector3 += new Vector3(0, 0.5f, 0);
        ShowBonus(vector3);
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

    private void ShowBonus(Vector3 vector3)
    {
        int bonusIndex = _random.Next(0, _bonuses.Count);
        _bonuses[bonusIndex].transform.position = vector3;
        _bonuses[bonusIndex].GetComponent<Effect>().SetEffectPosition(vector3);
        _bonuses[bonusIndex].SetActive(true);
    }

    private void GetCleanTiles()
    {
        _pieces = _piecesCreator.GetPiecesList();

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                if (_pieces[i, j] != null)
                {
                    _occupiedTiles.Add(_pointConverter.GridPoint(i, j));
                }
            }
        }

        foreach (var item in _occupiedByBonus)
        {
            _occupiedTiles.Add(item);
        }

        for (int i = 0; i < _board.MaxSideLength; i++)
        {
            for (int j = 0; j < _board.MaxSideLength; j++)
            {
                Vector2Int vector = _pointConverter.GridPoint(i, j);

                if (!_occupiedTiles.Contains(vector))
                    _cleanTiles.Add(vector);
            }
        }
    }

    public List<Vector2Int> GetCleanTilesList()
    {
        GetCleanTiles();
        List<Vector2Int> cleanTiles = _cleanTiles;
        return cleanTiles;
    }

    public void AddOccupiedTile(Vector2Int gridPoint)
    {
        _occupiedByBonus.Add(gridPoint);
    }

    public void ClearAllOccupiedTiles()
    {
        _occupiedTiles.Clear();
        _occupiedByBonus.Clear();
        _turnCount = 0;

        foreach (var item in _bonuses)
        {
            item.SetActive(false);
        }
    }

    public bool IsTileClearFromAbility(Vector2Int gridPoint)
    {
        return _occupiedByBonus.Contains(gridPoint) ? false : true;
    }

    public bool IsAbilityAt(Vector2Int gridPoint)
    {
        return _occupiedByBonus.Contains(gridPoint) ? true : false;
    }
}
