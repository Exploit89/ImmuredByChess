using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private BaseAttack _baseAttack;

    private void Awake()
    {
        Skill skill = Instantiate(_baseAttack, gameObject.transform);
        skill.name = _baseAttack.name;
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
