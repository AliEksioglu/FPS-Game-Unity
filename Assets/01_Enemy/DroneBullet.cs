using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    private Transform myT;
    private bool attack;
    private float attackDamage = DamagePoint.DroneBullet;
    [SerializeField] float speed;

    //Sounds
    [SerializeField] AudioSource hitDamageSound;
    //--
    void Start()
    {
        hitDamageSound.Play();
        myT = this.transform;
        StartCoroutine(DestroyTime());
    }

    void Update()
    {
            transform.position += myT.forward * -1 *  speed * Time.deltaTime;
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        hitDamageSound.Stop();
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.Player))
        {
            AttackDefiniton atackDef = new AttackDefiniton();
            atackDef.damagePoint = attackDamage;
            other.transform.GetComponentInParent<IDamagable>()?.GetDamagable(atackDef);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
