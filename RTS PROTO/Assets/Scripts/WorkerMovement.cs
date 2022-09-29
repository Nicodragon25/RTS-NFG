using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : MonoBehaviour
{
    GameObject DestinationManager;

    public List<Vector3> DestinationPoint = new List<Vector3>();
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    public LayerMask building;

    int destinationIndex;
    [SerializeField] int destinationIndexSave;
    public bool isMoving;

    private void Awake()
    {
        DestinationManager = GameObject.Find("UnitDestinationManager").gameObject;
    }
    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            destinationIndex = 0;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    DestinationPoint.Clear();
                    DestinationPoint.Add(hit.point);
                    DestinationManager.GetComponent<UnitDestinationManager>().CreateWalkableArea(gameObject, hit.point, "Square", 0);

                    foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                    {
                        unit.GetComponent<WorkerMovement>().myAgent.SetDestination(DestinationPoint[destinationIndex]);
                        destinationIndex++;
                    }
                    if (destinationIndex >= UnitSelections.Instance.unitSelected.Count) DestinationPoint.Clear();
                }
                else if (hit.collider.CompareTag("Resource"))
                {
                    DestinationManager.GetComponent<UnitDestinationManager>().PositionAroundResource(gameObject, hit.collider.transform.position, hit.collider.transform.localScale.x);

                    foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                    {
                        unit.GetComponent<WorkerActions>().ObjectWorkedOn = hit.collider.gameObject;
                        unit.GetComponent<WorkerMovement>().myAgent.SetDestination(DestinationPoint[destinationIndex]);
                        unit.GetComponent<WorkerMovement>().destinationIndexSave = destinationIndex;
                        destinationIndex++;
                    }
                    //if (destinationIndex >= UnitSelections.Instance.unitSelected.Count) DestinationPoint.Clear();
                }
                else if (hit.collider.CompareTag("Construction"))
                {
                    DestinationManager.GetComponent<UnitDestinationManager>().PositionAroundResource(gameObject, hit.collider.transform.position, hit.collider.transform.localScale.x);

                    foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                    {
                        unit.GetComponent<WorkerActions>().ObjectWorkedOn = hit.collider.gameObject;
                        unit.GetComponent<WorkerMovement>().myAgent.SetDestination(DestinationPoint[destinationIndex]);
                        unit.GetComponent<WorkerMovement>().destinationIndexSave = destinationIndex;
                        destinationIndex++;
                    }
                }

            }
        }

        if (myAgent.hasPath)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    
    public List<Transform> storages = new List<Transform>();
    public void DeliverResources(Vector3 startPosition)
    {
        foreach (StorageManager storage in FindObjectsOfType<StorageManager>())
        {
            storages.Add(storage.transform);
        }
        if(storages.Count > 0)
        {
        Vector3 destination;
        destination = GetClosestStorage(storages).position;
        transform.GetComponent<WorkerActions>().StorageBuilding = GetClosestStorage(storages).gameObject;
        if (destination.x < transform.position.x) destination.x++;
        if (destination.x > transform.position.x) destination.x--;
        if (destination.z < transform.position.z) destination.z++;
        if (destination.z > transform.position.z) destination.z--;
            myAgent.SetDestination(destination);
        }
    }
    public void GetBacktoResource()
    {
        myAgent.SetDestination(DestinationPoint[destinationIndexSave]);
        storages.Clear();
    }
    Transform GetClosestStorage(List<Transform> storages)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in storages)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
