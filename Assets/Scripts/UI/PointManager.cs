using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : Singleton<PointManager>
{
    public int initialPoint = 0;
    public int Strawberry;
    public int Points;
    public float TotalTime;
    public int tempPoints;
    private readonly string KEY_POINT = "Points";

    private void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt(KEY_POINT, initialPoint);
        tempPoints = initialPoint;
        LoadPoints();
    }


    public void PushTempPoints()
    {
        tempPoints = Points;
    }
    
    public void PullTempPoints()
    {
        Points = tempPoints;
    }

    private void LoadPoints()
    {
        Points= PlayerPrefs.GetInt(KEY_POINT);
    }

    public void AddPoints(int amount)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.PointClip, 1);
        Points += amount;
        PlayerPrefs.SetInt(KEY_POINT, Points);
        UIManager.Instance.UpdatePoint();
    }
    
    public void LossPoints(int amount)
    {
        Points -= amount;
        PlayerPrefs.SetInt(KEY_POINT, Points);
        UIManager.Instance.UpdatePoint();
    }

}