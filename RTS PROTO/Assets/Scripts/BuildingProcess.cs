using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProcess : MonoBehaviour
{
    public GameObject buildingPrefab;

    public float progress;
    void Start()
    {
        progress = 0;
    }

    void Update()
    {
        if(progress >= 100)
        {
            StartCoroutine(ConstructionCompletion());
        }
    }

    public void ConstructionProgress(float efficiency)
    {
        progress += efficiency;
    }

    IEnumerator ConstructionCompletion()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(buildingPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
