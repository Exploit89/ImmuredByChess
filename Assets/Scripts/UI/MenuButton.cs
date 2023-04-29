using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _menu.OpenPanel(_panel);
        _menuButton.gameObject.SetActive(false);
    }
}
