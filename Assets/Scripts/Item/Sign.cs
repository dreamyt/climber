using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject DialogBox;
    public float ReaminTime = 2.0f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DialogBox.SetActive(true);
            Invoke("Remove", ReaminTime);
        }
    }

    private void Remove()
    {
        DialogBox.SetActive(false);
    }
    
    
}
