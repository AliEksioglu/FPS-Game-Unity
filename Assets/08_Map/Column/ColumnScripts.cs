using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScripts : MonoBehaviour
{
    [SerializeField] bool Ters;
    private Transform myT;
    [SerializeField] private Transform one;
    [SerializeField] private Transform two;
    void Start()
    {
        myT = this.transform;
    }
    
    void FixedUpdate()
    {
        
    }

}
