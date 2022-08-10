using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    GameObject FormationManager;
    public List<Vector3> DestinationPoint = new List<Vector3>();
    NavMeshAgent myAgent;
    public LayerMask ground;

    public string shape;

    int destinationIndex;
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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                DestinationPoint.Add(hit.point);
                FormationManager.GetComponent<UnitDestinationManager>().CreateWalkableArea(gameObject, hit.point, shape);

                foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                {
                    unit.GetComponent<UnitMovement>().myAgent.SetDestination(DestinationPoint[destinationIndex]);
                    //destinationIndex += (destinationIndex + 1) % UnitSelections.Instance.unitSelected.Count;
                    destinationIndex++;
                }
                if (destinationIndex >= UnitSelections.Instance.unitSelected.Count) DestinationPoint.Clear();
            }
        }
    }
}
