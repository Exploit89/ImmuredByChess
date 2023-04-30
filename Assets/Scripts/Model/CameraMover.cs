using UnityEngine;


public class CameraMover : MonoBehaviour
{
    [SerializeField] private InputName _inputName;

    private Vector3 _startPosition = new Vector3(3, 50, -4.5f);
    private Vector3 _destination = new Vector3(3, 7.5f, -4.5f);
    private Vector3 _rotation = new Vector3(50, 0, 0);
    private float _speed = 8f;

    void Awake()
    {
        transform.position = _startPosition;
    }

    private void OnEnable()
    {
        _inputName.NameEntered += StartMove;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= StartMove;
    }

    private void StartMove()
    {
        StartCoroutine("Move");
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(_startPosition, _destination, Time.deltaTime * _speed);
        _startPosition = transform.position;
    }
}
