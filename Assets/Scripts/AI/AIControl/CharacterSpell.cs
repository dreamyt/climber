using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterSpell : CharacterComponents
{
    ObjectPooler Pooler;
    private float rotationAngle;
    public bool isSpelling = false;
    public float spellTime = 0.7f;
    public float spellFinishTime;
   
    
    private bool canSpell = true;
    [Header("Spell Settings")]
    [SerializeField] private Vector3 SpellGeneratePosition; // The real position to generate spell attack
    [SerializeField] private Vector3 spellGeneratePosition; // The relative position of spell compared to player
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Pooler = GetComponent<ObjectPooler>();
        
        
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        if (isSpelling)
        {
            if (Time.time >= spellFinishTime)
            {
                Attack();
                isSpelling = false;
            }
        }
        UpdateAnimations();
    }
    
    //used by ai
    public void useSpell()
    {
        if (!isSpelling && controller.isGrounded)   
        {
            spellFinishTime = Time.time + spellTime;
            isSpelling = true;
        }
    

        if (isSpelling)
        {
            if (Time.time >= spellFinishTime)
            {
                Attack();
                isSpelling = false;
            }
        }
    }
    
    private void Attack()
    {
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        if (GetComponent<CharacterFlip>().FacingRight)
        {
            SpellGeneratePosition.x = transform.position.x + spellGeneratePosition.x;
            SpellGeneratePosition.y = transform.position.y + spellGeneratePosition.y;
        }
        else
        {
            SpellGeneratePosition.x = transform.position.x - spellGeneratePosition.x;
            SpellGeneratePosition.y = transform.position.y + spellGeneratePosition.y;
        }
        projectilePooled.transform.position = SpellGeneratePosition;
        projectilePooled.SetActive(true);

        SpellAttack spellAttack = projectilePooled.GetComponent<SpellAttack>();
        if (GetComponent<CharacterFlip>().FacingRight)
        {
            spellAttack.facingRight = true;
        }
        else
        {
            spellAttack.TurnToLeft();
        }
        
    }
    
    private void UpdateAnimations()
    {
        if ((!currentHealth.dead) && (!currentHealth.getHit))
        {
            
            if (controller.isGrounded)
            {
                if (isSpelling)
                {
                    animator.SetBool("Spelling", true);
                }
                else
                {
                    animator.SetBool("Spelling", false);
                }
            }
        }
    }
    


}
