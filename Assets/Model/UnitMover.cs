using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInput))]

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Tilemap _board;
    [SerializeField] private Camera _camera;

    public Transform _position { get; private set; }

    public void OnClick()
    {
        _position = new GameObject().transform;
        _position.position = new Vector3();
        _position.position = _camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 1));

        Vector3Int cellPosition = _board.LocalToCell(_position.position);
        transform.localPosition = _board.GetCellCenterLocal(cellPosition);

        Vector3Int cellPositionAnother = _board.WorldToCell(transform.position);
        transform.position = _board.GetCellCenterWorld(cellPosition);
    }
}
