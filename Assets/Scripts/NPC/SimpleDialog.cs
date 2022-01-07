using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimpleDialog : MonoBehaviour
{
    public string[] words;
    public GameObject DialogBox;
    public Text DialogContent;

    private bool activated = false;

    private int count = 0;
    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown("space"))
            {
                DialogContent.text = words[count];
                count = (count + 1) % words.Length;
            }
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DialogBox.SetActive(true);
            activated = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DialogBox.SetActive(false);
        activated = false;
        
    }
    
}
