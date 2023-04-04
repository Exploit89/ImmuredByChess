using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Formation _formation;
    [SerializeField] private Unit _prefab;
    [SerializeField] private FigureSpawner _spawner;

    private Tilemap _tilemap;
    private int _maxPlayersCount = 2;

    public Transform PlayerStartPosition { get; private set; }
    public Transform EnemyStartPosition { get; private set; }

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        PlayerStartPosition = new GameObject().transform;
        EnemyStartPosition = new GameObject().transform;
        Debug.Log($"Размер Tilemap исходный = {_tilemap.size}");
        _tilemap.CompressBounds();
        Debug.Log($"Размер Tilemap после компрессии = {_tilemap.size}");
        SetStartPositions();
        Debug.Log($"Стартовая позиция игрока (белые) = {PlayerStartPosition.position}");
        Debug.Log($"Стартовая позиция врага (черные) = {EnemyStartPosition.position}");
        _formation.CreateFiguresPoints(PlayerStartPosition.position, EnemyStartPosition.position);
        _spawner.SpawnFigures();
    }

    private void SetStartPositions()
    {
        if (IsCorrectBoardSize())
        {
            PlayerStartPosition.position = new Vector3(_tilemap.localBounds.min.x, _tilemap.localBounds.min.y, 0);
            EnemyStartPosition.position = new Vector3(_tilemap.localBounds.min.x, _tilemap.localBounds.max.y - 1, 0);
        }
        else
            Debug.Log("Размер доски меньше допустимого");
    }

    private bool IsCorrectBoardSize()
    {
        int minBoardSizeX = _maxPlayersCount * _formation.MaxFiguresRows;
        int minBoarrdSizeY = _formation.MaxFiguresColumns;

        if(_tilemap.localBounds.max.x >= minBoardSizeX && _tilemap.localBounds.max.y >= minBoarrdSizeY)
            return false;
        else
            return true;
    }
}
