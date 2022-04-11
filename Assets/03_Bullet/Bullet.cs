using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : WeaponS
{
    LayerMask DamageLayer;
    [SerializeField] ParticleSystem hitEffect;
    private Transform bulletT;
    [SerializeField] private TrailRenderer Trail;
    [SerializeField] private float bulletSpeed;
    private float damagePoint = DamagePoint.PistolBullet;
    private void Awake()
    {
        bulletT = this.transform;
        DamageLayer = LayerMask.GetMask(Layers.DamageLayer);

    }

    float sayac = 0;
    private void Update()
    {
        sayac += Time.deltaTime;
        //bulletT.Translate(transform.forward  * bulletSpeed * Time.deltaTime);
        bulletT.position += bulletT.up * bulletSpeed * Time.deltaTime ;
        
        if (sayac > 2)
        {
            gameObject.SetActive(false);
            sayac = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == Tags.DamageTag)
        {

            AttackDefiniton attackDef = new AttackDefiniton();
            attackDef.damagePoint = damagePoint;
            other.transform.GetComponentInParent<IDamagable>()?.GetDamagable(attackDef);

            Instantiate(hitEffect,transform.position,Quaternion.identity);
            gameObject.SetActive(false);

        }
        else
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            print("rotation: " + transform.rotation);
            gameObject.SetActive(false);
        }
        
    }
    private void OnDisable()
    {
        Trail.Clear();
    }
    private void OnEnable()
    {
        
    }

}
