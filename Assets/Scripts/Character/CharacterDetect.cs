using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetect : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
      if (col.gameObject.CompareTag("Spike"))
      {
          health.dead = true;
      }
    }
}
