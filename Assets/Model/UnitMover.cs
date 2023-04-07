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
        _position.SetParent(_board.transform);
        _position.position = _camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(),9));
        Debug.Log(_position.position);

        Ray mouseRay = _camera.ScreenPointToRay(_position.position);
        Vector3 mousePos = mouseRay.GetPoint(8);
        Debug.Log(mousePos);
    }
}
