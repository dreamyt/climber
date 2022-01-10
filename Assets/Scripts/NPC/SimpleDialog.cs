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
    public float TimeBtwActivate = 3f;
    private bool activated = false;
    private bool canActivate = true;//prevent repeated activation of dialog box
    private int count = 0;
    void Update()
    {
        if (activated)
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.F))
            {
                DialogContent.text = words[count];
                count = (count + 1);
                if (count == words.Length)
                {
                    count = 0;
                    activated = false;
                    Time.timeScale = 1;
                    StartCoroutine(disableColliderTemp());
                }
            }
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (canActivate)
            {
                DialogBox.SetActive(true);
                activated = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DialogBox.SetActive(false);
        DialogContent.text = words[count];
        activated = false;
    }

    IEnumerator disableColliderTemp()
    {
        canActivate = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(TimeBtwActivate);
        GetComponent<BoxCollider2D>().enabled = true;
        canActivate = true;
    }
}
