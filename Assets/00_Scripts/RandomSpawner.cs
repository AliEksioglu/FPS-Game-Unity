using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject theDrone;
    float xPos;
    float zPos;
    void Start()
    {
        InvokeRepeating("EnemySpawner",2.0f,4f);
    }

    private void EnemySpawner()
    {
        xPos = Random.Range(-45f , 45f);
        zPos = Random.Range(-45f , 45f);
        Instantiate(theDrone, new Vector3(xPos, 20f, zPos),Quaternion.identity);
    }
}
