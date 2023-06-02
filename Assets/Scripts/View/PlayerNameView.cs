using TMPro;
using UnityEngine;

public class PlayerNameView : MonoBehaviour
{
    [SerializeField] private InputName _inputName;
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    private void OnEnable()
    {
        _inputName.NameEntered += ShowName;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= ShowName;
    }

    private void ShowName()
    {
        GetComponent<TMP_Text>().text = _pieceTurnMover.Player.Name;
    }
}
