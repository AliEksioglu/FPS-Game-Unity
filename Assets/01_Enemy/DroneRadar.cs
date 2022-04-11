using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRadar : MonoBehaviour
{
    [SerializeField] DroneManager target;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.RadarHedef))
        {
            target.followTarget = other.transform;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.RadarHedef))
        {
            target.followTarget = null;
        }
    }
}
