using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroneMove : MonoBehaviour
{
    private Transform myT;
    [SerializeField] float followDistance = 0f;
    [SerializeField] float speed;
    [SerializeField] float height;
    [SerializeField] DroneManager Target;
    void Start()
    {
        myT = this.transform;
    }

    // Update is called once per frame

    void Update()
    {
        if (Target.followTarget != null)
        {
            FollowTarget();
        }
        else
        {
            IdleMove();
        }

    }

    private void IdleMove()
    {
        
    }

    float turnDirection = -1;
    float time = 0f;
    float state = 0f;
    private void FollowTarget()
    {
        //cameraT.position + new Vector3(0, 3f, 0f);
        //Look Player
        Vector3 targetpoint = Target.followTarget.position;
        Vector3 direction = targetpoint - myT.position;
        myT.LookAt(targetpoint);
        Debug.DrawLine(transform.position, targetpoint, Color.white);
        float distance = Vector3.SqrMagnitude(myT.position - targetpoint);

        if (distance > followDistance * followDistance)
        {

            myT.position += direction.normalized * Time.deltaTime * speed;
        }
        else
        {

            if (distance < (followDistance * followDistance) - 0.5f)
            {
                myT.position += -direction.normalized * Time.deltaTime * speed;
            }

            if (time < 0)
            {
                state = Random.Range(0f, 1f);
                if (state < 0.51f)
                {
                    turnDirection = -turnDirection;
                    time = 4f;
                }
                else time = time / 2 + 1;
            }

            time -= Time.deltaTime;

            // 0 ' dan büyük ise aþaðýdan geliyor

            if (direction.normalized.y > -height ) //    -0.40    0.42
            {
                    myT.RotateAround(targetpoint, transform.right, Time.deltaTime * speed * 10);
            }
            else if (direction.normalized.y < -height - 0.02f)
            {
                    myT.RotateAround(targetpoint, -transform.right, Time.deltaTime * speed * 10);
            }
            else
            {
                myT.RotateAround(targetpoint, turnDirection * transform.up, Time.deltaTime * speed / 2 * 10);
            }
           
        }

    }

}
