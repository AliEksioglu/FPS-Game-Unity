using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextMeshShow : MonoBehaviour
{
    TextMesh textM;
    public void ShowFloatText(float showFloat)
    {
        textM = TextShowPooling.singleton.getTextMesh();
        textM.text = showFloat.ToString();
        textM.transform.position = this.transform.position;
        textM.gameObject.SetActive(true);
    }  
}
