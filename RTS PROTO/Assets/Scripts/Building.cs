using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask Buildings;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Buildings))
                {
                    Debug.Log(hit.collider.name);
                    HudManager.Instance.BuildingPanelSwitch(hit.collider.gameObject);
                }
            }
        }
    }

    public IEnumerator troopTraining(GameObject troopPrefab, float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 offset = new Vector3(3, 0, 5);
        Instantiate(troopPrefab, transform.position + offset, transform.rotation);
    }

    public void TroopTraining(GameObject troopPrefab)
    {
        StartCoroutine(troopTraining(troopPrefab, 3));
    }
}
