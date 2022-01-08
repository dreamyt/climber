using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float damage = 1f;
    //force applied to player
    public float force = 2f;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Vector2 direction = col.gameObject.transform.position - gameObject.transform.position;
            direction = direction.normalized;
            Debug.Log(direction);
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction*force);
        }
    }
}
