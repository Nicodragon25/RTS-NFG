using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    public float resourceHp;
    public float resourceSize;

    public enum ResourceTypes
    {
        Gold, Iron, Wood
    }
    public ResourceTypes ResourceType;
    void Update()
    {
        
    }

    public void VeinExploit(float dmg)
    {
        resourceHp -= dmg;
    }
}
