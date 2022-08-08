using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                    
                else if(Input.GetKey(KeyCode.LeftControl))UnitSelections.Instance.ControlClickDeselect(hit.collider.gameObject);
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && !EventSystem.current.IsPointerOverGameObject() && !UnitSelections.Instance.buildMode)
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
