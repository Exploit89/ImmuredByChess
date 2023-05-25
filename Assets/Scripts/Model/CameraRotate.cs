using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private HitPointBarCreator _hitPointBarCreator;
    private float _turnSpeed = 60.0f;
    private Vector3 _anchor;

    void Awake()
    {
        _anchor = new Vector3(4, 0, 4);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float delta = Input.GetAxis("Mouse X") * _turnSpeed * Time.deltaTime;
            transform.RotateAround(_anchor, Vector3.up, delta);
            _hitPointBarCreator.RotateHitPointBars(Vector3.up, delta);
        }
    }
}
