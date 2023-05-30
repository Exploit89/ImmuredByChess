using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private GameObject _PrepareScreenPanel;
    [SerializeField] private Button _battleButton;
    [SerializeField] private GameObject _abilitiesPanel;

    private void OnEnable()
    {
        _battleButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _battleButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _PrepareScreenPanel.gameObject.SetActive(false);
        _abilitiesPanel.SetActive(true);
    }
}
