using UnityEngine;
using UnityEngine.UI;

public class SkillToggle : MonoBehaviour
{
    [SerializeField] private Toggle _skillToggle;

    public string SkillName { get; private set; }

    private void OnEnable()
    {
        _skillToggle.onValueChanged.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _skillToggle.onValueChanged.RemoveListener(OnClick);
    }

    public void OnClick(bool isOn)
    {
        if(_skillToggle.isOn)
        {
            string skillName = _skillToggle.GetComponentInChildren<Text>().text;
            Debug.Log($"{skillName} chosen");
        }
    }

    public void SetName(string name)
    {
        SkillName = name;
        gameObject.GetComponentInChildren<Text>().text = SkillName;
    }
}
