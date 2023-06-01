using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private BaseAttack _baseAttack;
    [SerializeField] private Heal _heal;

    private List<Skill> _skillList;

    private void Awake()
    {
        _skillList = new List<Skill>();

        Skill baseAttack = Instantiate(_baseAttack, gameObject.transform);
        baseAttack.name = _baseAttack.name;
        _skillList.Add(baseAttack);
        Skill heal = Instantiate(_heal, gameObject.transform);
        heal.name = _heal.name;
        _skillList.Add(heal);
    }

    public List<Skill> GetSkills()
    {
        List<Skill> list = new List<Skill>();
        list = _skillList;
        return list;
    }
}
