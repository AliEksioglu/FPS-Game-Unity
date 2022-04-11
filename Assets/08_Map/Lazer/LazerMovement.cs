using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMovement : MonoBehaviour
{
    [SerializeField] Transform edge1;
    [SerializeField] Transform edge2;
    [SerializeField] Transform ObjectTransform;
    [SerializeField] float speed;
    float startTime, TotalDistance;
    private Vector3 movePosition;
    IEnumerator Start()
    {
        startTime = Time.time;
        while(true)
        {
            yield return RepeatLerp(edge1.position , edge2.position , 3.0f);
            yield return RepeatLerp(edge2.position , edge1.position , 3.0f);
        }
    }

    void Update()
    {


    }

    public IEnumerator RepeatLerp(Vector3 startPos , Vector3 endPos , float time) 
    {

        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            ObjectTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }


    }

}
