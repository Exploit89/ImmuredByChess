using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Item> _inventory;
    private string _name;
    private int _level;
    private int _experience;

    public int FiguresCount { get; private set; }

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
}
