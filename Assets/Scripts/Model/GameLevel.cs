using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    private int _currentLevel = 1;
    private int _maxLevel = 100;

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
    }

    public GameLevelSetup CreateEnemySetup(PieceType pieceType)
    {
        if (_currentLevel > _maxLevel)
            _currentLevel = _maxLevel;

        GameLevelSetup gameLevelSetup = new GameLevelSetup(_currentLevel, pieceType);
        return gameLevelSetup;
    }
}
