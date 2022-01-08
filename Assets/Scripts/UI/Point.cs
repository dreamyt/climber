using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int pointsToAdd = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointManager.Instance.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
