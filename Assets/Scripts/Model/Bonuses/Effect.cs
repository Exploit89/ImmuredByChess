using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private GameObject _bonusPrefab;

    private GameObject _bonusObject;

    public void Create()
    {
        _bonusObject = Instantiate(_bonusPrefab);
        _bonusObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        _bonusObject.transform.position = position;
    }

    public void Activate()
    {
        _bonusObject.SetActive(true);
        _bonusObject.GetComponentInChildren<ParticleSystem>().Play();
    }
}
