using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lav : MonoBehaviour
{

    private float damagePoint = DamagePoint.Lava;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layers.Player))
        {
            AttackDefiniton attack = new AttackDefiniton();
            attack.damagePoint = damagePoint;
            collision.transform.GetComponentInParent<IDamagable>()?.GetDamagable(attack);
        }
    }
}
