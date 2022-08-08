using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBluePrint : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    public LayerMask ground;

    public Material canBuildMat;
    public Material cantBuildMat;

    bool canBuild = true;
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            transform.position = hit.point;
        }
        UnitSelections.Instance.buildMode = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (canBuild) gameObject.GetComponent<MeshRenderer>().material = canBuildMat;
        if (!canBuild) gameObject.GetComponent<MeshRenderer>().material = cantBuildMat;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            transform.position = new Vector3(hit.point.x, transform.localScale.y / 2, hit.point.z);
            //if (hit.collider.name == "Ground") canBuild = true;
            //else canBuild = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canBuild)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Instantiate(prefab, transform.position, transform.rotation);
                    }
                    else
                    {
                        Instantiate(prefab, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }
                }
                UnitSelections.Instance.buildMode = false;
            }
        }
        if (Input.GetMouseButtonDown(1)) Destroy(gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name != "Ground") canBuild = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        canBuild = true;
    }
}
