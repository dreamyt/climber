using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthNumber;
    [SerializeField] private Text maxHealthNumber;
    [Header("Coin")]
    [SerializeField] private Text coinNumber;
    
    [Header("Point")]
    [SerializeField] private Text pointNumber;
    
    [Header("Time")] 
    [SerializeField] private Text elapsedTime;

    private float currentHealth;
    private float maxHealth;
    public int coins;
    private int points;
    private float time;
    private void Update()
    {
        InternalUpdateHealth();
        InternalUpdateCoin();
        InternalUpdatePoint();
        InternalUpdateTime();
    }
    
    public void UpdateHealth(float health, float maxHealth)
    {
        currentHealth = health;
        this.maxHealth = maxHealth;
    }

    public void UpdateCoin()
    {
        this.coins = CoinManager.Instance.Coins;
    }

    public void UpdatePoint()
    {
        this.points = PointManager.Instance.Points;
    }

    public void UpdateTime()
    {
        this.time = TimeManager.Instance.time;
    }
    private void InternalUpdateHealth() 
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, 10f * Time.deltaTime);
        healthNumber.text = currentHealth.ToString();
        maxHealthNumber.text = maxHealth.ToString();
    }

    private void InternalUpdateCoin()
    {
        coinNumber.text = coins.ToString();
    }

    private void InternalUpdatePoint()
    {
        pointNumber.text = points.ToString();
    }

    private void InternalUpdateTime()
    {
        elapsedTime.text = string.Format("{0:D2}:{1:D2}",
            (int)time / 60, (int)time % 60);
    }
}