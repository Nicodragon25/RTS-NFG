using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections instance;
    public static UnitSelections Instance { get { return instance; } }

    public bool buildMode;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
    }
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }
    public void ControlClickDeselect(GameObject unitToRemove)
    {
        if(unitSelected.Contains(unitToRemove))
        {
            unitToRemove.GetComponent<UnitMovement>().enabled = false;
            unitToRemove.transform.GetChild(0).gameObject.SetActive(false);
            unitSelected.Remove(unitToRemove);
        }
    }
    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }
    public void DragDeselect(GameObject unitToDeselect)
    {
        if (unitSelected.Contains(unitToDeselect))
        {
            unitSelected.Remove(unitToDeselect);
            unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
            unitToDeselect.GetComponent<UnitMovement>().enabled = false;
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            unit.transform.GetChild(0).gameObject.SetActive(false);
            unit.GetComponent<UnitMovement>().enabled = false;
        }
        unitSelected.Clear();
    }
}
