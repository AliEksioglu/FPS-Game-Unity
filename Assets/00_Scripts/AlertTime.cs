using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTime : MonoBehaviour
{
  
    public void ShowText()
    {
        this.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        StartCoroutine(sayac());
    }

    private IEnumerator sayac()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

}
