using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGathering : MonoBehaviour
{
    public GameObject ExploitingResource;
    public GameObject StorageBuilding;
    [SerializeField] float storageCapacity;
    [SerializeField] float actualStorage;
    [SerializeField] float GatherDmg;

    string carriedResource;
    bool isCollecting = false;
    bool isMoving;
    bool isCarrying;
    bool hasDelivered;
    private void Update()
    {
        isMoving = gameObject.GetComponent<WorkerMovement>().isMoving;
        if (ExploitingResource != null)
        {
            if (Vector3.Distance(transform.position, ExploitingResource.transform.position) < 3 && !isMoving)
            {
                if (carriedResource != null)
                {
                    if (ExploitingResource.GetComponent<ResourceController>().ResourceType.ToString() == carriedResource && !isCollecting)
                    {
                        Collect(ExploitingResource, GatherDmg);
                    }
                }
                else
                {
                    if (!isCollecting)
                    {
                        Collect(ExploitingResource, GatherDmg);
                    }
                }

            }
            if(actualStorage >= storageCapacity && !isCarrying)
            {
                gameObject.GetComponent<WorkerMovement>().DeliverResources(transform.position);
                isCarrying = true;
                hasDelivered = false;
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
        yield return new WaitForSeconds(2f);
        carriedResource = ResourceObject.GetComponent<ResourceController>().ResourceType.ToString();
        ResourceObject.GetComponent<ResourceController>().VeinExploit(efficiency);
        actualStorage += efficiency * 1.25f;
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
}
