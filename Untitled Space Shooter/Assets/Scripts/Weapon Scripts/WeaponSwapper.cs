using System.Collections;
using System.Collections.Generic;
using System;
using Core;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    public int currentWeapon;
    public float scrollWheelDelay;
    private float nextFire;

    private void Awake()
    {
        InputHandler.instance.inputActions.Player.WeaponSwap.performed += _ => ScrollWheelSelect();
        InputHandler.instance.inputActions.Player.PrimaryWeapon.performed += _ => currentWeapon = 0;
        InputHandler.instance.inputActions.Player.SecondaryWeapon.performed += _ => currentWeapon = 1;
        InputHandler.instance.inputActions.Player.TertiaryWeapon.performed += _ => currentWeapon = 2;
        InputHandler.instance.inputActions.Player.HolsterWeapon.performed += _ => currentWeapon = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon > transform.childCount - 1)
        {
            currentWeapon = 0;
        }

        if (currentWeapon < 0)
        {
            currentWeapon = transform.childCount - 1;
        }
        SwapWeapon();
    }

    private void SwapWeapon()
    {
        
        int i = 0;
        foreach (Transform weapons in transform)
        {
            weapons.gameObject.SetActive(i == currentWeapon);
            i++;
        }
        
    }

    private void ScrollWheelSelect()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + scrollWheelDelay;
            
            var x = InputHandler.instance.inputActions.Player.WeaponSwap.ReadValue<Vector2>();
            float scrollValue = x.y;

            switch (scrollValue)
            {
                case > 0:
                    currentWeapon++;
                    break;
                case < 0:
                    currentWeapon--;
                    break;
            }
        }
        
    }
}
