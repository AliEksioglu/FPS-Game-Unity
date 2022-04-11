using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DroneManager : MonoBehaviour, IDamagable
{
    public UnityEvent<float> damageEvent;
    public float damageCurHP { set {
            
            if(value < 0) throw new UnityException("Bu deger 0 ' dan kucuk olamaz ");

            _curHP -= value;
            if (_curHP <= 0) DroneExploded();

        }
        get { return _curHP; }
    }
    public float healthCurHP { set {
            _curHP += value;
            if (_curHP > maxHP) _curHP = maxHP;
        }
        get { return _curHP; }
    }
    public Transform followTarget { set { _followTarget = value; } get { return _followTarget; } }
    private Transform _followTarget;
    private float _curHP;
    [SerializeField] float maxHP = 1000f;
    [SerializeField] bool attacked;
 
    //--- Drone attack ---

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject droneFireBullet;
    [SerializeField] ParticleSystem[] particle;
    [SerializeField] Animator anim;

    private float cooldown;

    //---
    //Sound
    [SerializeField] AudioSource getDamage;
    [SerializeField] AudioSource hitDamage;
    [SerializeField] AudioSource destroyDrone;
    //---
    [SerializeField] ParticleSystem deadEffect;
    void Start()
    {
        followTarget = null;
        cooldown = 4f;
        healthCurHP = maxHP;
    }
    
    void Update()
    {
        if (followTarget != null)
        {
            if(cooldown < 0 && attacked)
            {
                fireBullet();
                cooldown = 4f;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }

        }
        
    }

    private void fireBullet()
    {

        anim.SetTrigger("attack");
        hitDamage.Play();
        for (int i = 0; i < particle.Length; i++) particle[i].Play();

        GameObject bullet = Instantiate(droneFireBullet);
        bullet.transform.position = firePoint.position;
        bullet.transform.forward = firePoint.position - followTarget.position;
    }

    public void GetDamagable(AttackDefiniton attackDefiniton)
    {
            getDamage.Play();
            float damage = attackDefiniton.damagePoint;
            damageCurHP = damage;
            damageEvent.Invoke(damage); 
    }
    public void DroneExploded()
    {
        EventManager.Event_OnDestroyDrone();
        Instantiate(deadEffect, transform.position , transform.rotation);
        GameManager.score++;
        Destroy(this.gameObject);
    }

    private void DestroyDroneSound()
    {
        destroyDrone.Play();
    }
    private void OnEnable()
    {
        EventManager.OnDestroyDrone += DestroyDroneSound;
    }
    private void OnDisable()
    {
        EventManager.OnDestroyDrone -= DestroyDroneSound;
    }
}
