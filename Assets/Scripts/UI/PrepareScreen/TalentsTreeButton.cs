using UnityEngine;
using UnityEngine.UI;

public class TalentsTreeButton : MonoBehaviour
{
    [SerializeField] private GameObject _talentsTreeScrollView;
    [SerializeField] private Button _talentsTreeButton;
    [SerializeField] private PrepareScreenPanel _prepareScreenPanel;

    private void OnEnable()
    {
        _talentsTreeButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _talentsTreeButton.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        _prepareScreenPanel.CloseAllPanels();
        _talentsTreeScrollView.gameObject.SetActive(true);
    }
}
