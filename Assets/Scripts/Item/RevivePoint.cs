using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePoint : MonoBehaviour
{
    public Transform Deadline;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.RevivePointClip, 1);
            col.gameObject.GetComponent<Health>().revive = gameObject.GetComponent<RevivePoint>();
        }
    }
}
