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
        Debug.Log("position = " + _position.position);
        Debug.Log($"{Mouse.current.position.x.ReadValue()} - {Mouse.current.position.y.ReadValue()}");

        //RaycastHit hit = new RaycastHit();
        //Ray ray = Camera.main.ScreenPointToRay(_position.position);

        //Vector3 oldPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);

        //var wtc = _board.WorldToCell(_position.position);
        //Debug.Log(wtc);

        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log("RayCast On");
        //    Debug.Log(hit.collider.gameObject.name);
        //    Debug.Log(hit.collider.gameObject.transform);
        //    Destroy(hit.collider.gameObject);
        //}

        _position.localPosition = _camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 1));
        Debug.Log("local position = " + _position.localPosition);

        Vector3 inversed = _board.transform.InverseTransformDirection(_position.position);
        Debug.Log("inversed = " + inversed);

        Vector3Int cell = _board.WorldToCell(_position.position);
        Debug.Log("cell = " + cell);
        Vector3 worldCell = _board.CellToWorld(cell);
        Debug.Log("world cell = " + worldCell);
    }

    ///Эти методы вызываются у тайлмап.  <summary>
    /// Эти методы вызываются у тайлмап.
    ///Сначала получил vector mouse через старую инпут в твоем случае новую.
    ///Потом у компонента тайлмап вызвал _map.WorldToCell(vector mouse) и получил vectorInt cell - это локальный целочисленный вектор тайла на который кликнул.
    ///А потом уже _map.CellToWorld(cell) (который возвращает vector) чтобы получить мировые координаты центра тайла.
    /// </summary>
}
