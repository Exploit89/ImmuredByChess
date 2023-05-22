using UnityEngine;

public class PrepareScreenPanel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _screenPanel;

    private void OnEnable()
    {
        _pieceTurnMover.GameLevelCompleted += OpenPanel;
    }

    private void OnDisable()
    {
        _pieceTurnMover.GameLevelCompleted -= OpenPanel;
    }

    private void OpenPanel()
    {
        _screenPanel.SetActive(true);
    }
}
