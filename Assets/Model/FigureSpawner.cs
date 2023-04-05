using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private List<Unit> _prefabs;
    [SerializeField] private Formation _formation;
    [SerializeField] private Player _player;

    private Dictionary<string, Vector3> _playerFiguresPositions = new Dictionary<string, Vector3>();

    //TODO запилить спавн с листа реально имеющихся фигур
    public void SpawnFigures()
    {
        _playerFiguresPositions = _formation.GetPlayerFiguresPoints();

        Vector3 rightRookPosition = new Vector3();
        _playerFiguresPositions.TryGetValue("RightRook", out rightRookPosition);

        //Instantiate(_prefabs, rightRookPosition, Quaternion.identity, _board.transform);
    }

    public void SpawnFigures2()
    {
        _playerFiguresPositions = _formation.GetPlayerFiguresPoints();
        List<Unit> _playerUnits = _player.GetUnits();

        foreach(Unit unit in _playerUnits)
        {
            _playerFiguresPositions.TryGetValue(unit.name, out Vector3 position);
            Instantiate(unit, position, Quaternion.identity, _board.transform);
        }
    }
}
