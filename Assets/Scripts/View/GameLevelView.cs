using System;
using TMPro;
using UnityEngine;

public class GameLevelView : MonoBehaviour
{
    [SerializeField] private GameLevel _gameLevel;
    [SerializeField] private InputName _inputName;

    private void OnEnable()
    {
        _gameLevel.LevelIncreased += ShowLevel;
        _inputName.NameEntered += ShowLevel;
    }

    private void OnDisable()
    {
        _gameLevel.LevelIncreased -= ShowLevel;
        _inputName.NameEntered -= ShowLevel;
    }

    private void ShowLevel()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = Convert.ToString("Этап " + _gameLevel.GetCurrentLevel());
    }
}
