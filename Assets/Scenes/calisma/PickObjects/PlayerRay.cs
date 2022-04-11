#pragma warning disable CS0108 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRay : MonoBehaviour
{
    const float rayDistance = 10f;
    GameObject currentOnRay = null;
    public float distancePickable;
    public Camera camera;
    Text Tooltip;
    [SerializeField] LayerMask layers;

    [SerializeField]
    Canvas canvas;

    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
        canvas.enabled = false;
        Tooltip = canvas.GetComponentInChildren<Text>();
    }
    float updateRate = .1f; //seconds 
    float counter = 0;

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter < updateRate)
            return;

        counter = 0;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hitInfo, rayDistance, layers))
        {
            distancePickable = Vector3.Distance(camera.transform.position, hitInfo.transform.position);
            canvas.enabled = true;
            currentOnRay = hitInfo.transform.gameObject;
            Tooltip.text = hitInfo.transform.name;
        }
        else
        {
            canvas.enabled = false;
            currentOnRay = null;
        }

    }

    public GameObject CurrentObject { get { return currentOnRay; } }

    private void OnDisable()
    {
        if (canvas)
            canvas.enabled = false;
        currentOnRay = null;
    }
}
