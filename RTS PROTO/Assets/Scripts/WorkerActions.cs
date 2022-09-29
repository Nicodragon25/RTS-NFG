using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerActions : MonoBehaviour
{
    public GameObject ObjectWorkedOn;
    public GameObject StorageBuilding;
    [SerializeField] float storageCapacity;
    [SerializeField] float actualStorage;
    [SerializeField] float GatherDmg;
    [SerializeField] float BuildEfficency;

    string carriedResource;
    bool isCollecting = false;
    bool isConstructing = false;
    bool isMoving;
    bool isCarrying;
    bool hasDelivered;
    private void Update()
    {
        isMoving = gameObject.GetComponent<WorkerMovement>().isMoving;
        if (ObjectWorkedOn != null)
        {
            if (ObjectWorkedOn.CompareTag("Resource") && Vector3.Distance(transform.position, ObjectWorkedOn.transform.position) < 3 && !isMoving)
            {
                if (carriedResource != null)
                {
                    if (ObjectWorkedOn.GetComponent<ResourceController>().resourceType.ToString() == carriedResource && !isCollecting)
                    {
                        Collect(ObjectWorkedOn, GatherDmg);
                    }
                }
                else
                {
                    if (!isCollecting)
                    {
                        Collect(ObjectWorkedOn, GatherDmg);
                    }
                }

            }
            else if(ObjectWorkedOn.CompareTag("Construction") && Vector3.Distance(transform.position, ObjectWorkedOn.transform.position) < 10 && !isMoving && !isConstructing)
            {
                Build(ObjectWorkedOn.gameObject, BuildEfficency);
            }

            if(actualStorage >= storageCapacity && !isCarrying)
            {
                gameObject.GetComponent<WorkerMovement>().DeliverResources(transform.position);
                if(StorageBuilding != null)
                {
                isCarrying = true;
                hasDelivered = false;
                }
            }
            if (StorageBuilding != null)
            {
                if (Vector3.Distance(transform.position, StorageBuilding.transform.position) < 2 && !isMoving && !hasDelivered)
                {
                    Deliver(StorageBuilding);
                    hasDelivered = true;
                }
            }
        }
    }
    void Collect(GameObject ResourceObject, float efficiency)
    {
        if (actualStorage < storageCapacity)
        {
            StartCoroutine(CollectCoroutine(ResourceObject, efficiency));
            isCollecting = true;
        }
    }
    void Deliver(GameObject Storage)
    {
        StartCoroutine(DeliverTime(Storage));
    }
    IEnumerator CollectCoroutine(GameObject ResourceObject, float efficiency)
    {
        switch (ResourceObject.name)
        {
            case "GoldOreVein":
        carriedResource = "gold";
                break;
            case "Wood":
        carriedResource = "wood";
                break;
            case "Stones":
                carriedResource = "stone";
                break;
            case "IronOreVein":
                carriedResource = "iron";
                break;
        }
        ResourceObject.GetComponent<ResourceController>().VeinExploit(efficiency);
        actualStorage += efficiency * 1.25f;
        yield return new WaitForSeconds(1f);
        isCollecting = false;
    }
    IEnumerator DeliverTime(GameObject Storage)
    {
        yield return new WaitForSeconds(0.5f);
        TotalResources.totalResources.Increase(actualStorage, carriedResource);
        actualStorage = 0;
        gameObject.GetComponent<WorkerMovement>().GetBacktoResource();
        isCarrying = false;
    }
    IEnumerator ConstructionCoroutine (GameObject ConstructionObject, float efficiency)
    {
        isConstructing = true;
        ConstructionObject.GetComponent<BuildingProcess>().ConstructionProgress(efficiency);
        yield return new WaitForSeconds(0.5f);
        isConstructing = false;
    }
    void Build(GameObject ConstructionObject, float efficiency)
    {
        if(ConstructionObject.GetComponent<BuildingProcess>().progress < 100 && !isConstructing)
        {
            StartCoroutine(ConstructionCoroutine(ConstructionObject, efficiency));
        }
    }
}
