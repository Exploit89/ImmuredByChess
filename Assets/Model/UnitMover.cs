using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInput))]

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Tilemap _board;
    [SerializeField] private Camera cam;

    public Transform _position { get; private set; }

    // переделать
    public void OnClick()
    {
        _position = new GameObject().transform;
        _position.position = new Vector3();
        _position.position = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 1));
        Debug.Log(_position.position);

        RaycastHit2D hit = Physics2D.Raycast(_position.position, transform.forward);
        bool hitt = Physics.Raycast(_position.position, transform.forward, out var hutt);
        Debug.Log(hutt.collider.gameObject.transform.position);
        Debug.Log(hit.collider.gameObject.name);
        Debug.Log(hit.collider.gameObject.transform.position);
        Debug.Log(_board.GetTile(Vector3Int.FloorToInt(hit.collider.gameObject.transform.position)));
    }
}
