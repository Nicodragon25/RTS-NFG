using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudManager : MonoBehaviour
{
    public GameObject ConstructionPanel;
    public GameObject LightInfantryPanel;
    public GameObject TroopControlPanel;
    public GameObject buildingBp;
    public GameObject Barrack;

    private static HudManager instance;
    public static HudManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            LightInfantryPanel.SetActive(false);
            if (!ConstructionPanel.activeInHierarchy) ConstructionPanel.SetActive(true);
            else ConstructionPanel.SetActive(false);
        }

        if (UnitSelections.Instance.unitSelected.Count > 0)
        {
            TroopControlPanel.SetActive(true);
            TroopControlPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(). text = "Selected Units : " + UnitSelections.Instance.unitSelected.Count;
        }
        else
        {
            TroopControlPanel.SetActive(false);
        }
    }

    public void Build(GameObject buildingPrefab)
    {
        if (buildingBp == null)
        {
            buildingBp = Instantiate(buildingPrefab);
        }
    }
    public void troopTrainButton(GameObject troopPrefab)
    {
        Barrack.GetComponent<Building>().TroopTraining(troopPrefab);
    }
    public void BuildingPanelSwitch(GameObject building)
    {
        switch (building.name)
        {
            case "Barrack(Clone)":
                ConstructionPanel.SetActive(false);
                Barrack = building;
                LightInfantryPanel.SetActive(true);
                break;

            case "Ground":
                LightInfantryPanel.SetActive(false);
                break;
        }
    }


    public void ShapeButton(string Shape)
    {
        foreach (var GameObject in GameObject.FindObjectsOfType<UnitMovement>())
        {
            GameObject.GetComponent<UnitMovement>().shape = Shape;
        }
    }
}
