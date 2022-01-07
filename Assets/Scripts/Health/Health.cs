using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //health related
    [Header("Health")]
    [SerializeField] private float initialHealth = 20f;
    [SerializeField] private float MaxHealth = 20f;
    [SerializeField] private bool destroyObject;
    public Text healthNumber;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public float previousHealth;
    //death and revive related
    [Header("death line and revive")] 
    public RevivePoint revive;
    
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public bool getHit = false;
    public float getHitTime = 0.15f;
    private float HitFinishTime;
   
    
    private float rendererEndInterval = 1.5f;
    private float rendererEndTime;
    private bool check = false;//used to ensure death check only execute once

    private Character character;
    private CharacterController controller;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private Animator anim;
    //private CharacterWeapon weapon;
    private EnemyHealth enemyHealth;
    public GameObject DeadNotice;
    public AudioSource HitAudio;
    private void Awake()
    {
        
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // weapon = GetComponent<CharacterWeapon>();
        //enemyHealth = GetComponent<EnemyHealth>();
        dead = false;
        getHit = false;
        health = initialHealth;
        maxHealth = MaxHealth;
        previousHealth = health;
       
        if (character.CharacterType == Character.CharacterTypes.player)
        {
            //UIManager.Instance.UpdateHealth(health, maxHealth);
            //healthNumber.text = health.ToString();
        }

    }

    private void Start()
    {
       
    }
    // Update is called once per frame
    private void Update()
    {
        if (dead)
        {
            
            Death();
            
            if (!destroyObject)
            {
                
               if (Input.GetKey("h"))
                {
                    //if(!CoinManager.Instance.isTimeout)
                        Revive();
                    
                }
               /*
                else if (Input.GetKey("r"))
                {
                    ReviveFromBeginning();
                    
                }*/
            }
        }
        else
        {
            if (character.CharacterType == Character.CharacterTypes.player)
            {
                //healthNumber.text = health.ToString();
            }

            IsDead();
            if (getHit)
            {
                if (Time.time > HitFinishTime)
                {
                    getHit = false;
                }
            }
        }

        UpdateAnimations();
    }
    private void IsDead()
    {
        if (dead)
        {
            return;
        }
        if (transform.position.y < revive.Deadline.position.y)
        {
            dead = true;
            health = 0;
            if (character.CharacterType == Character.CharacterTypes.player)
            {
                //UIManager.Instance.UpdateHealth(health, maxHealth);
                //healthNumber.text = health.ToString();
            }
        }
        if(health <= 0)
        {
            dead = true;
        }

        //if (CoinManager.Instance.isTimeout)
            //dead = true;
    }
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void Death()
    {
        if (destroyObject)
        {
            rigid.simulated = false;

            Invoke("DestroyObject", 1.5f);
        }
        else
        {
 
            if (!check)
            {
                rendererEndTime = Time.time + rendererEndInterval;
                check = true;
                rigid.simulated = false;
               // weapon.RemoveWeapon();
                //weapon.shootingAllowed = false;
                /*BGM.Stop();
                Gameover.Play();
                //destroy weapon*/
                DeadNotice.SetActive(true);
                
            }

            if (Time.time > rendererEndTime)
            {
                //spriteRenderer.enabled = false;
            }
        }
    }
    
    private void Revive()
    {
        /*
         Gameover.Stop();
         BGM.Play();
         weapon.ShowWeapon();
         weapon.ShootingAllowed = true;
         deadNotice.SetActive(false);
         */
        if (!destroyObject)
        {
           // weapon.ShowWeapon();
           // weapon.shootingAllowed = true;
            dead = false;
            rigid.simulated = true;
            //spriteRenderer.enabled = true;
            //anim.SetBool("Death", false);
            rigid.position = revive.transform.position;
            check = false;
            health = previousHealth;
            DeadNotice.SetActive(false);
            //healthNumber.text = health.ToString();
            //UIManager.Instance.UpdateHealth(health, maxHealth);
        }
    }
    private void ReviveFromBeginning()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            return;
        }

        //damage-shield
        getHit = true;

        //HitAudio.Play();
        
        HitFinishTime = Time.time + getHitTime;
        health -= damage;
        UpdateCharacterHealth();

    }

    private void UpdateCharacterHealth()
    {
        if (enemyHealth != null)
        {
            enemyHealth.UpdateEnemyHealth(health, maxHealth);
        }  
      
        // Update Player health
        if (character.CharacterType == Character.CharacterTypes.player)
        {
            healthNumber.text = health.ToString();
            //UIManager.Instance.UpdateHealth(health, maxHealth);
        }

    }
    private void UpdateAnimations()
    {
        if (anim != null)
        {
            if (dead)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Moving", false);
                anim.SetBool("Hurt", false);
                anim.SetBool("Spelling", false); // Spelling is stopped by Death
                anim.SetBool("Death", true);
            }
            else
            {
                if (getHit)
                {
                    anim.SetBool("Jump", false);
                    anim.SetBool("Moving", false);
                    anim.SetBool("Hurt", true);
                    anim.SetBool("Spelling", false); // Spelling is also stopped by Hurt
                }
                else
                {
                    anim.SetBool("Hurt", false);
                }
            }
        }
    }
    
}
