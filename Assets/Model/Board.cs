using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private Transform _whiteStartPosition;
    private Transform _blackStartPosition;
    private Tilemap _tilemap;

    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _whiteStartPosition = new GameObject().transform;
        _blackStartPosition = new GameObject().transform;
        _whiteStartPosition.position = new Vector3 (transform.position.x - 3.5f, _tilemap.transform.position.y - 2.5f);
        _blackStartPosition.position = new Vector3(transform.position.x - 3.5f, _tilemap.transform.position.y + 4.5f);
        Debug.Log($"Размер Tilemap = {_tilemap.size}");
        Debug.Log($"Размер клетки Tilemap = {_tilemap.cellSize}");
        Debug.Log($"Стартовая позиция игрока (белые) = {_whiteStartPosition.position}");
        Debug.Log($"Стартовая позиция врага (черные) = {_blackStartPosition.position}");
    }
}
