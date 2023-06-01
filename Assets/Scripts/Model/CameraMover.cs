using System.Collections;
using UnityEngine;


public class CameraMover : MonoBehaviour
{
    [SerializeField] private InputName _inputName;

    private Vector3 _startPosition = new Vector3(3, 50, -4.5f);
    private Vector3 _destination = new Vector3(3, 7.5f, -4.5f);
    private Quaternion _startRotation;
    private Quaternion _destinationRotation;
    private float _speed = 12f;
    private float _startXAngle = 10;
    private float _endXAngle = 50;
    private Coroutine _currentCoroutine;

    void Awake()
    {
        transform.position = _startPosition;
        _startRotation = Quaternion.Euler(_startXAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _destinationRotation = Quaternion.Euler(_endXAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = _startRotation;
    }

    private void OnEnable()
    {
        _inputName.NameEntered += Move;
    }

    private void OnDisable()
    {
        _inputName.NameEntered -= Move;
    }

    public IEnumerator ChangePosition()
    {
        while (transform.position != _destination || transform.rotation != _destinationRotation)
        {
            transform.position = Vector3.MoveTowards(_startPosition, _destination, Time.deltaTime * _speed);
            transform.rotation = Quaternion.RotateTowards(_startRotation, _destinationRotation, Time.deltaTime * _speed);
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            yield return null;
        }
    }

    public void Move()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        if (transform.position != _destination)
            _currentCoroutine = StartCoroutine(ChangePosition());
    }
}
