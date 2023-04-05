using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    private List<Item> _inventory;
    private string _name;
    private int _level;

    public void ChangeName(string name)
    {
        _name = name;
    }

    public List<Unit> GetUnits()
    {
        List<Unit> units = new List<Unit>();
        units = _units;
        return units;
    }
}
