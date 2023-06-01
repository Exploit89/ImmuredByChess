using UnityEngine;
using UnityEngine.Events;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private Skills _skills;

    private int _currentLevel = 1;
    private int _maxLevel = 100;

    public event UnityAction LevelIncreased;

    private void Awake()
    {
        _pieceTurnMover.GameLevelCompleted += IncreaseLevel;
    }

    private void OnDisable()
    {
        _pieceTurnMover.GameLevelCompleted -= IncreaseLevel;
    }

    private void IncreaseLevel()
    {
        _currentLevel++;
        LevelIncreased?.Invoke();
    }

    public GameLevelSetup CreateEnemySetup(PieceType pieceType)
    {
        if (_currentLevel > _maxLevel)
            _currentLevel = _maxLevel;

        GameLevelSetup gameLevelSetup = new GameLevelSetup(_currentLevel, pieceType, _skills.GetSkills());
        return gameLevelSetup;
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }
}
