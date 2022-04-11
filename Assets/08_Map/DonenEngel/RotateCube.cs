using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    Transform myT;
    public float speed;
    void Start()
    {
        myT = this.transform;
    }

    void Update()
    {
        myT.Rotate(0, speed * Time.deltaTime, 0);
    }
}
