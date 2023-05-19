using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillToggle : MonoBehaviour
{
    [SerializeField] private Toggle _skillToggle;

    //private List<Toggle> _toggles = new List<Toggle>(); 

    public string SkillName { get; private set; }

    public event UnityAction SkillChanged;

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
            SkillChanged?.Invoke();

            //GameObject parentPlayer = GameObject.FindGameObjectWithTag("PlayerPieces");
            //Transform[] piecesTransform = parentPlayer.GetComponentsInChildren<Transform>(true);

            //for (int i = 0; i <= _sideCount; i++)
            //{
            //    GameObject piece = piecesTransform[i + 1].gameObject;
            //    MovePieceToStartPosition(piece, i, 0);
            //}

        }
    }

    public void SetName(string name)
    {
        SkillName = name;
        gameObject.GetComponentInChildren<Text>().text = SkillName;
    }
}
