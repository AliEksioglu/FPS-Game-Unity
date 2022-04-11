using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextMeshManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] float TextMeshShowTime = 2f;
    void Start()
    {
        DOTween.Init();
        cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(cam.transform);
    }
    IEnumerator sayac()
    {
        yield return new WaitForSeconds(TextMeshShowTime);
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        transform
            .DOMoveY(transform.position.y + 2f, 1f);
        //seq.Join(transform.DOShakePosition(1f, new Vector3(0.5f, 0.1f, 0), randomness: 0));
        //seq.Join(transform.DOScale(0f, 2f));
        StartCoroutine(sayac());
    }
       
}
