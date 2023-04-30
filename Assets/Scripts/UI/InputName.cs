using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _panel;

    public event UnityAction NameEntered;

    public void OnSetName()
    {
        _pieceTurnMover.Player.SetName(_inputField.text);
        _inputField.gameObject.SetActive(false);
        _menu.ClosePanel(_panel);
        _closeButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
        NameEntered?.Invoke();
    }
}
