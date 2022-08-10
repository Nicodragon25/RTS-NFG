using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public float goldStorage;
    public float woodStorage;
    public float ironStorage;

    public void addResources(float quantity, string resource)
    {
        switch (resource)
        {
            case "Gold":
                goldStorage += quantity;
                break;
        }

    }
}
