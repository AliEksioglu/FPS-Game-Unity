using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class deneme : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(-90, 0, 0), 20f , RotateMode.LocalAxisAdd);
    }
    private void Update()
    {
    }
}
