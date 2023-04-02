using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Player))]

public class Board : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Tilemap _tilemap;
    private int _possibleFiguresRows = 2;
    private int _possibleFiguresColumns = 8;
    private int _possiblePlayersCount = 2;

    public Transform PlayerStartPosition { get; private set; }
    public Transform EnemyStartPosition { get; private set; }

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        PlayerStartPosition = new GameObject().transform;
        EnemyStartPosition = new GameObject().transform;
        PlayerStartPosition.position = new Vector3 (transform.position.x - 3.5f, _tilemap.transform.position.y - 2.5f);
        EnemyStartPosition.position = new Vector3(transform.position.x - 3.5f, _tilemap.transform.position.y + 4.5f);
        Debug.Log($"Размер Tilemap = {_tilemap.size}");
        Debug.Log($"Размер клетки Tilemap = {_tilemap.cellSize}");
        Debug.Log($"Стартовая позиция игрока (белые) = {PlayerStartPosition.position}");
        Debug.Log($"Стартовая позиция врага (черные) = {EnemyStartPosition.position}");
        SetPlayerStartPosition();
        Debug.Log($"Стартовая позиция игрока (белые) = {PlayerStartPosition.position}");
    }

    private void SetPlayerStartPosition()
    {
        if (TryGetCorrectBoardSize())
        {
            int xLength = _tilemap.size.x;
            int yLength = _tilemap.size.y;
            int xStartPosition = Convert.ToInt32(_tilemap.transform.position.x) - (xLength - (_possibleFiguresRows * _possiblePlayersCount)) / _possiblePlayersCount;
            int yStartPosition = yLength - (_player.FiguresCount / _possibleFiguresRows); //TODO допилить
            PlayerStartPosition.position = new Vector3(xStartPosition, yStartPosition);
        }
        else
        {
            Debug.Log("Размер доски меньше допустимого");
        }
    }

    private bool TryGetCorrectBoardSize()
    {
        if(_tilemap.size.x < _possibleFiguresRows * _possiblePlayersCount || _tilemap.size.y < _possibleFiguresColumns)
            return false;
        else
            return true;
    }
}
