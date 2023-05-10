using System;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private InputName _inputName;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private ExperienceCalculator _experienceCalculator;

    private string _name;
    private int _level;
    private int _currentExperience;
    private int _nextLevelExperience;

    private void OnEnable()
    {
        _inputName.NameEntered += ShowName;
        _inputName.NameEntered += ShowLevel;
        _inputName.NameEntered += ShowExperience;
        _pieceTurnMover.ExperienceIncreased += ShowExperience;
        _pieceTurnMover.LevelIncreased += ShowLevel;
        _pieceTurnMover.LevelIncreased += ShowExperience;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= ShowName;
        _inputName.NameEntered -= ShowLevel;
        _inputName.NameEntered -= ShowExperience;
        _pieceTurnMover.ExperienceIncreased -= ShowExperience;
        _pieceTurnMover.LevelIncreased -= ShowLevel;
        _pieceTurnMover.LevelIncreased -= ShowExperience;
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
        _currentExperience = _pieceTurnMover.Player.Experience;
        _nextLevelExperience = _experienceCalculator.GetNextLevelExperience(_pieceTurnMover.Player.Level);
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == "PlayerExperience")
                text.text = Convert.ToString(_currentExperience) + " / " + Convert.ToString(_nextLevelExperience);
        }
    }
}
