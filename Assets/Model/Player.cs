using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    private List<Item> _inventory;
    private string _name;
    private int _level;
    private int _experience;

    public void ChangeName(string name)
    {
        _name = name;
    }
    
    public void ChangeExperience(int experience)
    {
        _experience += experience;
    }

    public void IncreaseLevel()
    {
        _level++;
    }

    public List<Unit> GetUnits()
    {
        List <Unit> units = new List<Unit>();
        units = _units;
        return units;
    }
}
