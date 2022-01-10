using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimeManager : Singleton<TimeManager>
{
    public float time = 0f;

    private void Start()
    {
        StartCoroutine(Count());
    }

    private IEnumerator Count()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!GameObject.Find("Player").GetComponent<Health>().dead)
            {
                time++;
            }
            UIManager.Instance.UpdateTime();
        }
    }
    
}
