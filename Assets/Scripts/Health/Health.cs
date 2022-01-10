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
    private CharacterWeapon _characterWeapon;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    //private CharacterWeapon weapon;
    private EnemyHealth enemyHealth;
    public GameObject DeadNotice;
    public AudioSource HitAudio;
    private void Awake()
    {
        
        character = GetComponent<Character>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        _characterWeapon = GetComponent<CharacterWeapon>();
        enemyHealth = GetComponent<EnemyHealth>();
        dead = false;
        getHit = false;
        health = initialHealth;
        maxHealth = MaxHealth;
        previousHealth = health;
       
        if (character.CharacterType == Character.CharacterTypes.player)
        {
            UIManager.Instance.UpdateHealth(health, maxHealth);
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
                   Revive();
               }
               else if(!check)
               {
                   Death();
               }
            }
            else
            {
                Death();
            }
        }
        else
        {
            IsDead();
            if (getHit)
            {
                if (character.CharacterType == Character.CharacterTypes.player)
                {
                    timeSpentBlink += Time.deltaTime;

                    float remainder = timeSpentBlink % 0.3f;
                    spriteRenderer.enabled = remainder > 0.15f;
                }
                if (Time.time > HitFinishTime)
                {
                    spriteRenderer.enabled = true;
                    getHit = false;
                }
            }
        }
    }
    private void IsDead()
    {
        if (dead)
        {
            return;
        }

        if (character.CharacterType == Character.CharacterTypes.player)
        {
            if (transform.position.y < revive.Deadline.position.y)
            {
                dead = true;
                health = 0;
            }
        }
        else
        {
            if (transform.position.y < -20)
            {
                dead = true;
                health = 0;
            }
        }

        if(health <= 0)
        {
            dead = true;
        }
        
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
            SoundManager.Instance.PlaySound(SoundManager.Instance.DieClip, 1);
            UIManager.Instance.UpdateHealth(0, maxHealth);
            rigid.simulated = false;
            DeadNotice.SetActive(true);
            spriteRenderer.enabled = false;
            GameObject obj = Instantiate(diePaticle, transform.position, transform.rotation);
            Destroy(obj, 2f);
            _characterWeapon.RemoveWeapon();
            _characterWeapon.shootingAllowed = false;
            check = true;
            /*BGM.Stop();
            Gameover.Play();
            */
        }
    
    }
    public void Revive()
    {
        /*
         Gameover.Stop();
         BGM.Play();
         */
        if (!destroyObject)
        {
           _characterWeapon.ShowWeapon();
           _characterWeapon.shootingAllowed = true;
            check = false;
            dead = false;
            health = previousHealth;
            rigid.simulated = true;
            rigid.position = revive.transform.position;
            spriteRenderer.enabled = true;
            DeadNotice.SetActive(false);
            UIManager.Instance.UpdateHealth(health, maxHealth);
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
            UIManager.Instance.UpdateHealth(health, maxHealth);
        }

    }

}
