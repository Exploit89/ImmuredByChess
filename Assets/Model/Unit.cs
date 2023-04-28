using UnityEngine;

[RequireComponent(typeof(Piece))]

public class Unit : MonoBehaviour
{
    private Piece _piece;
    private string _name;
    private string _description;
    private int _level;
    private float _health;
    private float _mana;

    private void OnEnable()
    {
        _piece = GetComponent<Piece>();
        _name = _piece.Type.ToString();
    }
}
