using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public LayerMask layerDetected;
    public float detectRadius = 5f;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool finished = false;//if chat is over
    private bool activated = false;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!activated && Physics2D.OverlapCircle(transform.position, detectRadius, layerDetected))
        {
            _spriteRenderer.enabled = true;
            _animator.SetTrigger("Appear");
            activated = true;
        }

    }

  
}
