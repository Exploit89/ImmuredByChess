using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Unit _prefab;
    [SerializeField] private Formation _formation;

    private Dictionary<string, Vector3> _playerFiguresPositions = new Dictionary<string, Vector3>();

    //TODO запилить спавн с листа реально имеющихся фигур
    public void SpawnFigures()
    {
        _playerFiguresPositions = _formation.GetPlayerFiguresPoints();

        Vector3 rightRookPosition = new Vector3();
        _playerFiguresPositions.TryGetValue("RightRook", out rightRookPosition);

        Instantiate(_prefab, rightRookPosition, Quaternion.identity, _board.transform);
    }
}
