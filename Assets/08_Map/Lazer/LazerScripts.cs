using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScripts : MonoBehaviour 
{
    RaycastHit _outHit;
    private LayerMask Player;
    [SerializeField] LayerMask _layers;
    private LineRenderer _line;
    private Transform myT;
    [SerializeField] private float damagePoint = DamagePoint.Lazer;
    private void Start()
    {
        //Ground = LayerMask.GetMask(Layers.Ground);
        Player = LayerMask.GetMask(Layers.Player);
        _line = GetComponent<LineRenderer>();
        myT = transform;
    }
    private void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward,out _outHit, Mathf.Infinity, _layers))
        {
            _line.enabled = true;
            _line.SetPosition(0, myT.position); _line.SetPosition(1, _outHit.point);
            _line.startWidth = 0.3f + Mathf.Sin(Time.time)/30;
            _line.endWidth = 0.1f + Mathf.Sin(Time.time)/30;

            AttackDefiniton attack = new AttackDefiniton();
            attack.damagePoint = damagePoint;
            _outHit.transform.GetComponentInParent<IDamagable>()?.GetDamagable(attack);

        }
        else
        {
            _line.enabled = false;
        }


    }



}
