using UnityEngine;

public class PrepareScreenPanel : MonoBehaviour
{
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private GameObject _screenPanel;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _talentsTree;
    [SerializeField] private GameObject _shop;

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
        _inventory.SetActive(false);
        _talentsTree.SetActive(false);
        _shop.SetActive(false);
    }

    public void CloseAllPanels()
    {
        _inventory.SetActive(false);
        _talentsTree.SetActive(false);
        _shop.SetActive(false);
    }
}
