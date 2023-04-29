using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _menu.ClosePanel(_panel);
        _closeButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);
    }
}
