using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] InputName _inputName;
    [SerializeField] PieceTurnMover _pieceTurnMover;

    private string _name;

    void OnEnable()
    {
        _inputName.NameEntered += SetName;
    }

    void OnDisable()
    {
        _inputName.NameEntered -= SetName;
    }

    public void SetName()
    {
        _name = _pieceTurnMover.Player.Name;
        Debug.Log(_name);
        TMP_Text playerName = GetComponentInChildren<TMP_Text>();
        playerName.text = _name;
    }
}
