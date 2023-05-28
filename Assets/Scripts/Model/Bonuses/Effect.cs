using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private GameObject _bonusPrefab;

    private GameObject _bonusEffect;

    public void CreateEffect()
    {
        _bonusEffect = Instantiate(_bonusPrefab);
        _bonusEffect.SetActive(false);
    }

    public void SetEffectPosition(Vector3 position)
    {
        _bonusEffect.transform.position = position;
    }

    public void ActivateEffect()
    {
        _bonusEffect.SetActive(true);
        _bonusEffect.GetComponentInChildren<ParticleSystem>().Play();
    }
}
