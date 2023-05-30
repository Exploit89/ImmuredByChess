using UnityEngine;

public class PrepareScreenPanel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _screenPanel;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _talentsTree;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _abilitiesPanel;

    private void OnEnable()
    {
        _pieceTurnMover.GameLevelCompleted += OpenPanel;
        _pieceTurnMover.GameLevelLost += OpenPanel;
    }

    private void OnDisable()
    {
        _pieceTurnMover.GameLevelCompleted -= OpenPanel;
        _pieceTurnMover.GameLevelLost -= OpenPanel;
    }

    private void OpenPanel()
    {
        _screenPanel.SetActive(true);
        _inventory.SetActive(false);
        _talentsTree.SetActive(false);
        _shop.SetActive(false);
        _abilitiesPanel.SetActive(false);
    }

    public void CloseAllPanels()
    {
        _inventory.SetActive(false);
        _talentsTree.SetActive(false);
        _shop.SetActive(false);
    }
}
