using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;
    [SerializeField] private ItemData itemWeaponData;
    // The reference of Weapon being used by player
    [HideInInspector]
    public Weapon CurrentWeapon { get; set; }
    //public Weapon SecondaryWeapon { get; set; }
    [HideInInspector]
    public Weapon SecondaryWeapon;
    [HideInInspector]
    public bool shootingAllowed = true;
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
        if(CoinManager.Instance.isWeaponBought)
            SecondaryWeapon = itemWeaponData.WeaponToEquip;
    }

    
    protected override void Update()
    {
        base.Update();
    }
    

    protected override void HandleInput()
    {
        
        if (shootingAllowed)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
        {
            EquipWeapon(weaponToUse , weaponHolderPosition);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
        {
            EquipWeapon(SecondaryWeapon, weaponHolderPosition);
        }
    }

    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.TriggerShot();
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if(CurrentWeapon!=null)
        {
            //Destroy(GameObject.Find("Pool"));
            Destroy(CurrentWeapon.gameObject);
            //CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
            if (GetComponent<Movement>().side==1)
            {
                CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
            }
            else if (GetComponent<Movement>().side==-1)
            {
                Quaternion flip = Quaternion.Euler(0f, 180, 0);
                CurrentWeapon = Instantiate(weapon, weaponPosition.position, flip);
            }
        }
        else
        {
            CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        }
        
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
    }

    public void ShowWeapon()
    {
        CurrentWeapon.ShowWeapon();
    }

    public void RemoveWeapon()
    {
        CurrentWeapon.RemoveWeapon();
    }
}
