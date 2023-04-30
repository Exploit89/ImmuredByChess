using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_InputField _inputName;

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
        _startButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _inputName.gameObject.SetActive(true);
    }
}
