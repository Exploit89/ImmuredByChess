using UnityEngine;
using UnityEngine.Tilemaps;

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
        Debug.Log($"������ Tilemap = {_tilemap.size}");
        _tilemap.CompressBounds();
        Debug.Log($"������ Tilemap = {_tilemap.size}");
        SetStartPositions();
        Debug.Log($"��������� ������� ������ (�����) = {PlayerStartPosition.position}");
        Debug.Log($"��������� ������� ����� (������) = {EnemyStartPosition.position}");
    }

    private void SetStartPositions()
    {
        if (IsCorrectBoardSize())
        {
            PlayerStartPosition.position = new Vector3(_tilemap.localBounds.min.x, _tilemap.localBounds.min.y, 0);
            EnemyStartPosition.position = new Vector3(_tilemap.localBounds.min.x, _tilemap.localBounds.max.y - 1, 0);
        }
        else
            Debug.Log("������ ����� ������ �����������");
    }

    private bool IsCorrectBoardSize()
    {
        Vector3 minBoardSize = new Vector3(_possiblePlayersCount * _possibleFiguresRows, _possibleFiguresColumns, 0);

        if(_tilemap.localBounds.max.x >= minBoardSize.x && _tilemap.localBounds.max.y >= minBoardSize.y)
            return false;
        else
            return true;
    }
}
