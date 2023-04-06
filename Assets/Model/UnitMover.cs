using System;
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
        _position.position = _camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 1));
        Debug.Log(_position.position);

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(_position.position);

        Vector3 oldPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);

        var wtc = _board.WorldToCell(_position.position);
        Debug.Log(wtc);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("RayCast On");
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.transform);
            Destroy(hit.collider.gameObject);
        }
    }
}
