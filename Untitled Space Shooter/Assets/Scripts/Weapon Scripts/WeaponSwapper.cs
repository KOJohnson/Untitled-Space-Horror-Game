using System.Collections;
using System.Collections.Generic;
using System;
using Core;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    [SerializeField]private int currentWeapon;
    [SerializeField]private float scrollWheelDelay;
    private float _nextFire;

    private void Awake()
    {
        PlayerInputManager.InputActions.Player.WeaponSwap.performed += _ => ScrollWheelSelect();
        PlayerInputManager.InputActions.Player.PrimaryWeapon.performed += _ => currentWeapon = 0;
        PlayerInputManager.InputActions.Player.SecondaryWeapon.performed += _ => currentWeapon = 1;
        PlayerInputManager.InputActions.Player.TertiaryWeapon.performed += _ => currentWeapon = 2;
        PlayerInputManager.InputActions.Player.HolsterWeapon.performed += _ => currentWeapon = 3;
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
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + scrollWheelDelay;
            
            var scrollWheelInput = PlayerInputManager.InputActions.Player.WeaponSwap.ReadValue<Vector2>();
            float scrollValue = scrollWheelInput.y;

            switch (scrollValue)
            {
                case > 0:
                    currentWeapon--;
                    break;
                case < 0:
                    currentWeapon++;
                    break;
            }
        }
        
    }
}
