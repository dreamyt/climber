using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public Text Coin;
    public Text Point;
    public Text Straw;
    public Text Time;

    private void Start()
    {
        Coin.text = CoinManager.Instance.Coins.ToString();
        Point.text = PointManager.Instance.Points.ToString();
        Straw.text = PointManager.Instance.Strawberry.ToString();
        Time.text = string.Format("{0:D2}:{1:D2}",
            (int)PointManager.Instance.TotalTime / 60, (int)PointManager.Instance.TotalTime % 60);
    }
}
