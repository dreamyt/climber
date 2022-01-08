using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    [Header("death related")] 
    public RevivePoint revive;
    public GameObject diePaticle;
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public bool getHit = false;
    public float getHitTime = 0.3f;
    private float HitFinishTime;
    private bool check = false;//used to ensure death check only execute once
    private float timeSpentBlink = 0.01f;

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
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
            if (!destroyObject)
            {
                
               if (Input.GetKey(KeyCode.H))
               {
                    //if(!CoinManager.Instance.isTimeout)
                    Revive();
               }
               else if(!check)
               {
                   Death();
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

            if (Input.GetKeyDown(KeyCode.I))
            {
                TakeDamage(1);
            }
            IsDead();
            if (getHit)
            {
                timeSpentBlink += Time.deltaTime;
                
                float remainder = timeSpentBlink % 0.3f;
                spriteRenderer.enabled = remainder > 0.15f;
                
                if (Time.time > HitFinishTime)
                {
                    spriteRenderer.enabled = true;
                    getHit = false;
                }
            }
        }

        //UpdateAnimations();
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
        Debug.Log(check);
        if (destroyObject)
        {
            rigid.simulated = false;
            Invoke("DestroyObject", 1.5f);
        }
        else
        {
            rigid.simulated = false;
            DeadNotice.SetActive(true);
            spriteRenderer.enabled = false;
            GameObject obj = Instantiate(diePaticle, transform.position, transform.rotation);
            Destroy(obj, 2f);
            // weapon.RemoveWeapon();
            //weapon.shootingAllowed = false;
            /*BGM.Stop();
            Gameover.Play();
            //destroy weapon*/
            check = true;
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
            check = false;
            dead = false;
            health = previousHealth;
            rigid.simulated = true;
            rigid.position = revive.transform.position;
            spriteRenderer.enabled = true;
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
            //healthNumber.text = health.ToString();
            //UIManager.Instance.UpdateHealth(health, maxHealth);
        }

    }
    /*private void UpdateAnimations()
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
    }*/
    
}
