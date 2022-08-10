using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalResources : MonoBehaviour
{
    public static TotalResources totalResources;

    public float gold;
    public float wood;
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
        gold += quantity;
        HudManager.Instance.ChangeText(HudManager.Instance.Resources.transform.GetChild(0).gameObject, gold.ToString());
    }
}
