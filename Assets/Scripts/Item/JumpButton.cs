using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public float jumpForce = 5f;
    public float kDisabledDelay = 2f;
    private GameObject player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(col.gameObject.GetComponent<Collision>().onGround)
                Debug.Log(col.gameObject.GetComponent<Collision>().onGround);
            player = col.gameObject;
            anim.SetBool("Bounce", true);
            bounce();
        }
    }
    
    private void bounce()
    {
        if (player != null)
        {
            player.GetComponent<BetterJumping>().bounced = true;
            player.GetComponentInChildren<AnimationScript>().SetTrigger("jump");
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, jumpForce);
            StartCoroutine(setBounceToFalse());
        }
    }

    IEnumerator setBounceToFalse()
    {
        yield return new WaitForSeconds(kDisabledDelay);
        player.GetComponent<BetterJumping>().bounced = false;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        anim.SetBool("Bounce", false);
    }

}
