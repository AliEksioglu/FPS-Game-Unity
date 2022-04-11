using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickable : MonoBehaviour
{
    Vector3 spawnPoint;
    public GameObject RespawnPickable;
    Transform myT;
    void Start()
    {
        spawnPoint = transform.parent.position;
        myT = this.transform;
    }

    void Update()
    {
        if(myT.position.y < -35)
        {
            Instantiate(RespawnPickable, spawnPoint, Quaternion.identity, transform.parent);
            Destroy(this.gameObject);
        }
    }

}
