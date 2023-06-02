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
    private PointConverter _gridPoints;
    private Vector3 _gridOffset = new Vector3(0.5f, 0, 0.5f);
    private Vector3 _bonusOffset = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        _gridPoints = new PointConverter();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnClick);
        _pieceTurnMover.GameLevelCompleted += DestroyAbility;
        _pieceTurnMover.GameLevelLost += DestroyAbility;
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnClick);
        _pieceTurnMover.GameLevelCompleted -= DestroyAbility;
        _pieceTurnMover.GameLevelLost -= DestroyAbility;
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
                    _toggle.GetComponentInChildren<Text>().text = $"Способность {_abilityNumber + 1}";
                    _toggle.isOn = false;
                    _bonusCreator.AddOccupiedTile(gridPoint);
                }
            }
        }
        else
        {
            if (_ability == null)
            {
                return;
            }
        }
    }

    private void OnClick(bool isOn)
    {
        if (_toggle.isOn)
        {
            List<GameObject> abilities = _pieceTurnMover.Player.GetItems();
            List<Toggle> toggles = _abilityToggle.GetToggles();

            if (abilities.Count > _abilityNumber)
            {
                toggles[_abilityNumber].GetComponentInChildren<Text>().text = abilities[_abilityNumber].name;
                _ability = Instantiate(abilities[_abilityNumber]);
                _ability.tag = "BoardAbility";
                //Destroy(_ability.GetComponent<Effect>());
                _ability.GetComponent<Effect>().enabled = false;
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

    private void DestroyAbility()
    {
        Destroy(_ability);
        _ability = null;
    }

    public void SetAbilityNumber(int number)
    {
        _abilityNumber = number;
    }
}
