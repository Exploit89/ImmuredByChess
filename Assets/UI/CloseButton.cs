using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _menu.ClosePanel(_panel);
        _menuButton.gameObject.SetActive(true);
    }
}
