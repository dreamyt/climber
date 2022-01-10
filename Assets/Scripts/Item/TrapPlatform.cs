using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D _collider2D;
    public AudioSource TrapPlatformAudio;
    public bool Back = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player" && transform.position.y < other.gameObject.transform.position.y)
        {
            //TrapPlatformAudio.Play();
            anim.SetBool("Collapse", true);
        }
    }
    
    void Collapse()
    {
        _collider2D.enabled = false;
        if (Back)
        {
            StartCoroutine(backToOrigin());
        }
    }

    IEnumerator backToOrigin()
    {
        yield return new WaitForSeconds(1.5f);
        _collider2D.enabled = true;
        anim.SetBool("Collapse", false);
    }
}
