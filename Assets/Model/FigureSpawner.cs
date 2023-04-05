using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Formation _formation;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private Dictionary<string, Vector3> _playerFiguresPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Vector3> _enemyFiguresPositions = new Dictionary<string, Vector3>();

    public void SpawnFigures()
    {
        _playerFiguresPositions = _formation.GetPlayerFiguresPoints();
        _enemyFiguresPositions = _formation.GetEnemyFiguresPoints();
        List<Unit> _playerUnits = _player.GetUnits();
        List<Unit> _enemyUnits = _enemy.GetUnits();

        foreach(Unit unit in _playerUnits)
        {
            _playerFiguresPositions.TryGetValue(unit.name, out Vector3 position);
            Instantiate(unit, position, Quaternion.identity, _board.transform);
        }

        foreach(Unit unit in _enemyUnits)
        {
            _enemyFiguresPositions.TryGetValue(unit.name, out Vector3 position);
            Instantiate(unit, position, Quaternion.identity, _board.transform);
        }
    }
}
