using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSeller : MonoBehaviour
{
    [SerializeField] private ItemData itemWeaponData;
    [SerializeField] public int coins = 10;
    private CharacterWeapon _characterWeapon;
    public GameObject Dialog;//buy info
    public Text text;//content in the dialog
    public string[] words;
    private bool met = false;//if the character collides with the merchant
    private bool selected = false;
    private bool bought = false;

    private void Start()
    {
            
    }

    private void Update()
    {
        if (met)
        {
            Time.timeScale = 0;
            if (!selected)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(CoinManager.Instance.Coins>=coins)
                    {
                        bought = true;
                        CoinManager.Instance.LossCoins(coins);
                    }
                    selected = true;
                   
                }
            }
            else
            {
                if (bought)
                {
                    if(_characterWeapon!=null)
                    {
                        _characterWeapon.SecondaryWeapon = itemWeaponData.WeaponToEquip;
                    }

                    text.text = words[1];
                    met = false;
                }
                else
                {
                    text.text = words[2];
                    met = false;
                    selected = false;
                }
                Time.timeScale = 1;
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //show the dialog
        //check if the coin is enough
        //prompt bought success or not
        //if success, set the secondary weapon
        if (collision.CompareTag("Player"))
        {
            if (!selected)
            {
                Dialog.SetActive(true);
                text.text = words[0];
                met = true;
                _characterWeapon = collision.GetComponent<CharacterWeapon>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        met = false;
        Dialog.SetActive(false);
        text.text = words[0];
    }

}

