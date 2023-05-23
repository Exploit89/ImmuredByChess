using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _shopScrollView;
    [SerializeField] private Button _shopButton;
    [SerializeField] private PrepareScreenPanel _prepareScreenPanel;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _prepareScreenPanel.CloseAllPanels();
        _shopScrollView.gameObject.SetActive(true);
    }
}
