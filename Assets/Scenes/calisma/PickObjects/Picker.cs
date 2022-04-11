#pragma warning disable CS0108
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField]
    PlayerRay playerRay;
    [SerializeField]
    Camera camera;

    [SerializeField]
    public float power = 5f;
    [SerializeField]
    public float distance = 2f;

    IPickable targetPickable = null;
    private float Distance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetPickable != null) //release if already carrying something
                CarryToggle(false);

            targetPickable = playerRay.CurrentObject?.GetComponent<IPickable>();

            if (targetPickable != null && !targetPickable.isCarried )
            {
                Distance = playerRay.distancePickable;
                CarryToggle(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(targetPickable!=null)
            CarryToggle(false);
        }
    }

    private void FixedUpdate()
    {
        if (targetPickable != null)
        {
            
            Vector3 targetDirection = (camera.transform.position + camera.transform.forward * Distance) - targetPickable.rigidbody.position; ;
            targetPickable.rigidbody.velocity = targetDirection * power;
            
        }
    }

    public void CarryToggle(bool value)
    {
        if (value)//if true
        {
            targetPickable.picker = this;
            targetPickable.isCarried = value;
            targetPickable.rigidbody.useGravity = !value;
            targetPickable.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            targetPickable.isCarried = value;
            targetPickable.rigidbody.useGravity = !value;
            targetPickable.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            targetPickable.picker = null;
            targetPickable = null;
        }
    }
}
