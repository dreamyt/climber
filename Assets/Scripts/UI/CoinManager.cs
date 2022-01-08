using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] int initialCoins = 0;
    public int Coins;
    public int tempCoins;
    public AudioSource audio; 
    public bool isWeaponBought = false;
    private readonly string KEY_COIN = "Coins";

    private void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt(KEY_COIN, initialCoins);
        tempCoins = initialCoins;
        LoadCoins();
    }


    public void PushTempCoins()
    {
        tempCoins = Coins;
    }
    
    public void PullTempCoins()
    {
        Coins = tempCoins;
    }

    private void LoadCoins()
    {
        Coins = PlayerPrefs.GetInt(KEY_COIN);
    }

    public void AddCoins(int amount)
    {
        audio.Play();
        Coins += amount;
        PlayerPrefs.SetInt(KEY_COIN, Coins);
        UIManager.Instance.UpdateCoin();
    }
    
    public void LossCoins(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt(KEY_COIN, Coins);
        UIManager.Instance.UpdateCoin();
    }

}