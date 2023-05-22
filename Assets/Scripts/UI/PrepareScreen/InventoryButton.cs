using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryScrollView;
    [SerializeField] private Button _inventoryButton;

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
        _inventoryScrollView.gameObject.SetActive(true);
    }
}
