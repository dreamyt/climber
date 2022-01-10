using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int ScroesToAdd = 2;
    private Health enemyHealth;
    private Animator anim;
    private float enemyCurrentHealth;
    private float enemyMaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateAnimations();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            TakeDamage(col.GetComponent<ReturnToPool>().damage);
        }
        
    }

    private void TakeDamage(float damage)
    {
        //Hit once,add points
        PointManager.Instance.AddPoints(ScroesToAdd);
        enemyHealth.TakeDamage(damage);
    }

    private void UpdateHealth()
    {
        //health bar update
    }

    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        enemyCurrentHealth = currentHealth;
        enemyMaxHealth = maxHealth;
    }
    
    private void UpdateAnimations()
    {
        if (enemyHealth.dead)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Moving", false);
            anim.SetBool("Hurt", false);
            anim.SetBool("Spelling", false);  // Spelling is stopped by Death
            anim.SetBool("Death", true);
        }
        else
        {
            if (enemyHealth.getHit)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Moving", false);
                anim.SetBool("Hurt", true);
                anim.SetBool("Spelling", false);  // Spelling is also stopped by Hurt
            }
            else
            {
                anim.SetBool("Hurt", false);
            }
        }
    }
}
