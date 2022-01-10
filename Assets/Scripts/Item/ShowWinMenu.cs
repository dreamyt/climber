using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowWinMenu : MonoBehaviour
{
    private Animator animator;
    public GameObject WinMenu;
    public GameObject Star0;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (animator != null)
            {

                SoundManager.Instance.PlaySound(SoundManager.Instance.WinClip, 1);
                PointManager.Instance.TotalTime = TimeManager.Instance.time;
                animator.SetTrigger("Win");
                StartCoroutine("WinScene");
            }
        }
    }

    IEnumerator WinScene()
    {
        yield return new WaitForSeconds(1.5f);
        WinMenu.SetActive(true);
        ScoreGrading(PointManager.Instance.Points);
    }

    private void ScoreGrading(float scores)
    {
        if (scores <= 50)
        {
            Star0.SetActive(true);
        }
        else if (scores <= 100)
        {
            Star1.SetActive(true);
        }
        else if (scores <= 200)
        {
            Star2.SetActive(true);
        }
        else
        {
            Star3.SetActive(true);
        }
    }
}
