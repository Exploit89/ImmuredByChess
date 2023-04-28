using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _menu.ClosePanel(_panel);
    }
}
