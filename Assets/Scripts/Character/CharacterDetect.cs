using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetect : MonoBehaviour
{
    private Health health;
    private Rigidbody2D rigid;
    private Collision _collision;
    private void Start()
    {
        health = GetComponent<Health>();
        rigid = GetComponent<Rigidbody2D>();
        _collision = GetComponent<Collision>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemySpell"))
        {
            health.TakeDamage(col.GetComponent<SpellReturnToPool>().damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //spike
      if (collision.gameObject.CompareTag("Spike"))
      {
          health.dead = true;
      }
      
      // 被敌人Enemy碰到
      if (collision.gameObject.layer == LayerMask.NameToLayer("Fox"))
      {
          // if in the air, check if it stamps the fox
          if (!_collision.onGround)
          {
              float w = 1.2f;
              Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position + _collision.bottomOffset,
                  new Vector2(w, 0.8f), 0,
                  LayerMask.GetMask("Fox"));
              
              foreach (Collider2D c in colliders)
              {
                  Health rd = c.GetComponent<Health>();
                  if (rd != null)
                  {
                      rd.dead = true;
                      PointManager.Instance.AddPoints(5);
                      // bounce back
                      rigid.velocity = new Vector2(rigid.velocity.x, 0);
                      rigid.AddForce(new Vector2(0, 300));
                  }
              }

              if (colliders.Length > 0)
              {
                  return;
              }
          }

          // if not stamps the fox, get hurt
          GetComponent<Health>().TakeDamage(1);
      }
    }
}
