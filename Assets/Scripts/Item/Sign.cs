using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public String info;
    public GameObject DialogBox;
    public Text text;
    public float RemainTime = 2.5f;
    public bool pause = false;
    private bool activated = false;
    public bool seeAgain = true;
    private void Update()
    {
        if (pause&&activated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 1;
                DialogBox.SetActive(false);
                pause = false;
                StartCoroutine(DisableTemporarily());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            activated = true;
            text.text = info;
            DialogBox.SetActive(true);
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                StartCoroutine(Remove());    
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        activated = false;
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(RemainTime);
        DialogBox.SetActive(false);
        StartCoroutine(DisableTemporarily());
    }

    IEnumerator DisableTemporarily()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        if (seeAgain)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    
}
