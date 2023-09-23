using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class UnitSelections : MonoBehaviour
{

    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();
    public GameObject status;
    public TextMeshProUGUI nameShip;
    public TextMeshProUGUI attackShip;
    public TextMeshProUGUI defenseShip;
    public TextMeshProUGUI healthPoint;

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);

        Status(unitToAdd);
    }

    public void DeselectAll()
    {
        status.SetActive(false);
        nameShip.text = "-";
        attackShip.text = "-";
        defenseShip.text = "-";
        healthPoint.text = "-";

        unitsSelected.Clear();
    }

    private void Status(GameObject unitToAdd)
    {
        status.SetActive(true);

        Ship script = unitToAdd.GetComponent<Ship>();

        nameShip.text = script.GetName();
        attackShip.text = script.GetAttack();
        defenseShip.text = script.GetDefense();
        healthPoint.text = script.GetHealthPoints();
    }
}
