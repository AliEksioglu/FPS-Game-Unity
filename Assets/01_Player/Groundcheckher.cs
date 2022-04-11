using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheckher : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {

        if ((collision.gameObject.layer== LayerMask.NameToLayer(Layers.Ground)  || collision.gameObject.layer == LayerMask.NameToLayer(Layers.Pickable) )
            && collision.contacts[0].normal.y > 0.7f )
        {
              PlayerMovement.isgorunded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        PlayerMovement.isgorunded = false;
    }

}
