using System;
using TMPro;
using UnityEngine;

public class PlayerExperienceView : MonoBehaviour
{
    [SerializeField] private InputName _inputName;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private ExperienceCalculator _experienceCalculator;

    private void OnEnable()
    {
        _inputName.NameEntered += ShowExperience;
        _pieceTurnMover.ExperienceIncreased += ShowExperience;
        _pieceTurnMover.LevelIncreased += ShowExperience;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= ShowExperience;
        _pieceTurnMover.ExperienceIncreased -= ShowExperience;
        _pieceTurnMover.LevelIncreased -= ShowExperience;
    }

    private void ShowExperience()
    {
        int playerLevel = _pieceTurnMover.Player.Level;
        string currentExperience = Convert.ToString(_pieceTurnMover.Player.Experience);
        string nextLevelExperience = Convert.ToString(_experienceCalculator.GetNextLevelExperience(playerLevel));
        GetComponent<TMP_Text>().text = currentExperience + " / " + nextLevelExperience;
    }
}
