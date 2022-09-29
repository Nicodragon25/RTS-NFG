using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalResources : MonoBehaviour
{
    public static TotalResources totalResources;

    public float gold;
    public float wood;
    public float stone;
    public float iron;
    private void Awake()
    {
        if (totalResources != null && totalResources != this)
        {
            Destroy(gameObject);
        }
        else
        {
            totalResources = this;
        }
    }

    public void Increase(float quantity, string resourceType)
    {
        switch (resourceType)
        {
            case "gold":
                gold += quantity;
                HudManager.Instance.ChangeText(GameObject.Find("GoldText").gameObject, gold.ToString());
                break;
            case "wood":
                wood += quantity;
                HudManager.Instance.ChangeText(GameObject.Find("WoodText").gameObject, wood.ToString());
                break;
            case "stone":
                stone += quantity;
                HudManager.Instance.ChangeText(GameObject.Find("StoneText").gameObject, stone.ToString());
                break;
            case "iron":
                iron += quantity;
                HudManager.Instance.ChangeText(GameObject.Find("IronText").gameObject, iron.ToString());
                break;
            default:
                break;
        }
    }
}
