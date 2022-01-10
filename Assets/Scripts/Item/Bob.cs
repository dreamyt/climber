using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float damage = 1f;
    //force applied to player
    public float force = 2f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<Health>().TakeDamage(damage);
            Vector2 direction = col.transform.position - transform.position;
            //direction = direction.normalized;
            col.GetComponent<Rigidbody2D>().AddForce(direction*force);
        }
    }
}

