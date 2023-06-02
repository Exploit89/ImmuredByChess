using System;
using TMPro;
using UnityEngine;

public class PlayerLevelView : MonoBehaviour
{
    [SerializeField] private InputName _inputName;
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    private void OnEnable()
    {
        _inputName.NameEntered += ShowLevel;
        _pieceTurnMover.LevelIncreased += ShowLevel;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= ShowLevel;
        _pieceTurnMover.LevelIncreased -= ShowLevel;
    }

    private void ShowLevel()
    {
        GetComponent<TMP_Text>().text = Convert.ToString(_pieceTurnMover.Player.Level);
    }
}
