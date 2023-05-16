using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private BaseAttack _baseAttack;

    private void Awake()
    {
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
        Instantiate(_baseAttack, gameObject.transform);
    }
}
