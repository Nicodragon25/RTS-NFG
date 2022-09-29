using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    public GameObject destinationFlag;
    GameObject FormationManager;
    public List<List<Vector3>> ListOfDestinations = new List<List<Vector3>>();
    public List<Vector3> DestinationPoint = new List<Vector3>();
    public List<Vector3> nextPos = new List<Vector3>();
    public List<Vector3> thirdPos = new List<Vector3>();
    NavMeshAgent myAgent;
    public LayerMask ground;

    public string shape;


    Vector3 destination;
    float dist;

    bool isLastDestination;
    public int listOfDestinationsIndex;
    int destinationIndex;
    int nextPosListDestinationIndex;
    private void Awake()
    {
        FormationManager = GameObject.Find("UnitDestinationManager").gameObject;
        shape = "Square";
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
            isLastDestination = false;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    ListOfDestinations.Clear();
                    listOfDestinationsIndex = 0;
                    ListOfDestinations.Add(new List<Vector3>());
                    ListOfDestinations[listOfDestinationsIndex].Add(hit.point);

                    Instantiate(destinationFlag, hit.point, transform.rotation);
                    FormationManager.GetComponent<UnitDestinationManager>().CreateWalkableArea(gameObject, hit.point, shape, listOfDestinationsIndex);
                    foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                    {
                        unit.GetComponent<UnitMovement>().myAgent.SetDestination(ListOfDestinations[listOfDestinationsIndex][destinationIndex]);
                        destination = ListOfDestinations[listOfDestinationsIndex][destinationIndex];
                        destinationIndex++;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    ListOfDestinations.Add(new List<Vector3>());
                    listOfDestinationsIndex++;

                    ListOfDestinations[listOfDestinationsIndex].Add(hit.point);

                    Instantiate(destinationFlag, hit.point, transform.rotation);
                    FormationManager.GetComponent<UnitDestinationManager>().CreateWalkableArea(gameObject, hit.point, shape, listOfDestinationsIndex);
                }
            }
        }
        if (destination != null)
            dist = Vector3.Distance(destination, transform.position);

        if (nextPosListDestinationIndex >= ListOfDestinations.Count - 1)
        {
            isLastDestination = true;
            nextPosListDestinationIndex = 0;
        }

        if (dist < 1 && destination != null && ListOfDestinations.Count > 0 && !isLastDestination)
        {
            destinationIndex = 0;
            nextPosListDestinationIndex++;
            foreach (GameObject unit in UnitSelections.Instance.unitSelected)
            {
                unit.GetComponent<UnitMovement>().myAgent.SetDestination(ListOfDestinations[nextPosListDestinationIndex][destinationIndex]);
                destination = ListOfDestinations[nextPosListDestinationIndex][destinationIndex];
                destinationIndex++;
            }
        }
    }
}
