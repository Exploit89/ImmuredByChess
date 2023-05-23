using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryScrollView;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private PrepareScreenPanel _prepareScreenPanel;

    private void OnEnable()
    {
        _inventoryButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _inventoryButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _prepareScreenPanel.CloseAllPanels();
        _inventoryScrollView.gameObject.SetActive(true);
    }
}
