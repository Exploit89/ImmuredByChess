using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Formation _formation;
    [SerializeField] private FigureSpawner _spawner;

    private Tilemap _tilemap;
    private int _maxPlayersCount = 2;

    public Transform PlayerStartPosition { get; private set; }
    public Transform EnemyStartPosition { get; private set; }

    private void Awake()
    {
        PlayerStartPosition = new GameObject().transform;
        EnemyStartPosition = new GameObject().transform;
        _tilemap = GetComponent<Tilemap>();
        _tilemap.CompressBounds();
        SetStartPositions();
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
