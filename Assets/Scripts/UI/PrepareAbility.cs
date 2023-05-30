using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareAbility : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private PieceTurnMover _pieceTurnMover;
    [SerializeField] private AbilityToggle _abilityToggle;
    [SerializeField] private BonusCreator _bonusCreator;

    private int _abilityNumber;
    private GameObject _ability;
    private Vector3 _gridOffset = new Vector3(0.5f, 0, 0.5f);
    private Vector3 _bonusOffset = new Vector3(0, 0.5f, 0);
    private PointConverter _gridPoints;

    private void Start()
    {
        _gridPoints = new PointConverter();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnClick);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && _toggle.isOn)
        {
            Vector3 point = hit.point + _gridOffset;
            Vector2Int gridPoint = _gridPoints.GridFromPoint(point);
            _ability.SetActive(true);
            _ability.transform.position = _gridPoints.PointFromGrid(gridPoint);
            _ability.transform.position += _bonusOffset;

            if (Input.GetMouseButtonDown(0))
            {
                if (_bonusCreator.GetCleanTilesList().Contains(gridPoint))
                {
                    Debug.Log("стена установлена");
                    _toggle.GetComponentInChildren<Text>().text = $"Способность {_abilityNumber + 1}";
                    _toggle.isOn = false;
                    _bonusCreator.AddOccupiedTile(gridPoint);
                }
            }
        }
        else
        {
            if(_ability == null)
            {
                return;
            }
        }
    }

    private void OnClick(bool isOn)
    {
        if (_toggle.isOn)
        {
            string skillName = _abilityToggle.GetComponentInChildren<Text>().text;
            Debug.Log($"Ability {skillName} chosen");

            List<GameObject> abilities = _pieceTurnMover.Player.GetItems();
            List<Toggle> toggles = _abilityToggle.GetToggles();

            if(abilities.Count > _abilityNumber)
            {
                toggles[_abilityNumber].GetComponentInChildren<Text>().text = abilities[_abilityNumber].name;
                _ability = Instantiate(abilities[_abilityNumber]);
                Destroy(_ability.GetComponent<Effect>());
                Destroy(_ability.GetComponent<Bonus>());
                _ability.SetActive(isOn);
                _pieceTurnMover.Player.RemoveItem(_abilityNumber);
            }
            else
            {
                _toggle.isOn = false;
            }
        }
    }

    public void SetAbilityNumber(int number)
    {
        _abilityNumber = number;
    }
}
