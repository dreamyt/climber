using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public GameObject virtualCam;
    //have been triggered once
    private bool isSet=false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            virtualCam.SetActive(true);
            isSet = true;
        }
        
        
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            virtualCam.SetActive(false);
        }
        
    }
}