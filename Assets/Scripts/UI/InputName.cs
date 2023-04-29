using TMPro;
using UnityEngine;

public class InputName : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private PieceTurnMover _pieceTurnMover;

    public void OnSetName()
    {
        _pieceTurnMover.Player.SetName(_inputField.text);
        Debug.Log(_pieceTurnMover.Player.Name);
        _inputField.gameObject.SetActive(false);
    }
}
