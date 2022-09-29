using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    public float resourceHp;
    public float resourceSize;

    public string resourceType;
    void Update()
    {
        
    }

    public void VeinExploit(float dmg)
    {
        resourceHp -= dmg;
    }
}
