using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInput))]

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Tilemap _board;
    [SerializeField] private Camera cam;

    public Transform _position { get; private set; }

    public void OnClick()
    {
        _position = new GameObject().transform;
        _position.position = new Vector3();
        _position.position = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 1));
        Debug.Log(_position.position);

        //Vector3Int cellPosition = _board.LocalToCell(_position.position);
        //transform.localPosition = _board.GetCellCenterLocal(cellPosition);
        //Debug.Log(transform.localPosition);

        Vector3Int cellPosition = _board.WorldToCell(transform.position);
        transform.position = _board.GetCellCenterWorld(cellPosition);
        Debug.Log(transform.position);
    }
}
