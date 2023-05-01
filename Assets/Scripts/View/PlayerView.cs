using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] InputName _inputName;
    [SerializeField] PieceTurnMover _pieceTurnMover;

    private string _name;
    private int _level;
    private int _experience;

    void OnEnable()
    {
        _inputName.NameEntered += ShowName;
        _inputName.NameEntered += ShowLevel;
    }

    void OnDisable()
    {
        _inputName.NameEntered -= ShowName;
        _inputName.NameEntered -= ShowLevel;
    }

    private void ShowName()
    {
        _name = _pieceTurnMover.Player.Name;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            if(text.name == "PlayerName")
                text.text = _name;
        }
    }

    private void ShowLevel()
    {
        _level = _pieceTurnMover.Player.Level;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            if (text.name == "PlayerLevel")
                text.text = Convert.ToString(_level);
        }
    }

    private void ShowExperience()
    {

    }
}
