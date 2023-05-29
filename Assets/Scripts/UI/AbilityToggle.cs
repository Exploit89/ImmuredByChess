using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityToggle : MonoBehaviour
{
    [SerializeField] PieceTurnMover _pieceTurnMover;

    private List<GameObject> _abilities = new List<GameObject>();
    private List<Toggle> _toggles = new List<Toggle>();

    private void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            _toggles.Add(toggles[i]);
            Debug.Log(toggles[i]);
        }
    }

    private void OnEnable()
    {
        _pieceTurnMover.Player.ItemsChanged += ShowAbilities;
    }

    private void OnDisable()
    {
        _pieceTurnMover.Player.ItemsChanged -= ShowAbilities;
    }

    private void ShowAbilities()
    {
        List<GameObject> abilities = _pieceTurnMover.Player.GetItems();

        for (int i = 0; i < abilities.Count; i++)
        {
            _toggles[i].GetComponentInChildren<Text>().text = abilities[i].name;
        }
    }
}
