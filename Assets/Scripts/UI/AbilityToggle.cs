using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityToggle : MonoBehaviour
{
    [SerializeField] PieceTurnMover _pieceTurnMover;

    private List<Toggle> _toggles = new List<Toggle>();

    private void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].GetComponent<PrepareAbility>().SetAbilityNumber(i);
            _toggles.Add(toggles[i]);
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

    public List<Toggle> GetToggles()
    {
        List<Toggle> toggles = _toggles;
        return toggles;
    }
}
