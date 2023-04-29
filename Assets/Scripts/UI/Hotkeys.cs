using UnityEngine;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _menuButton;

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            _panel.SetActive(true);
            _menuButton.gameObject.SetActive(false);
        }
    }
}
