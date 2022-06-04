using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    public int currentWeapon;
    public float scrollWheelDelay;
    private float nextFire;

    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        
        playerInput.Player.WeaponSwap.performed += _ => ScrollWheelSelect();
        playerInput.Player.PrimaryWeapon.performed += _ => currentWeapon = 0;
        playerInput.Player.SecondaryWeapon.performed += _ => currentWeapon = 1;
        playerInput.Player.TertiaryWeapon.performed += _ => currentWeapon = 2;
        playerInput.Player.HolsterWeapon.performed += _ => currentWeapon = 3;
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
            
            var x = playerInput.Player.WeaponSwap.ReadValue<Vector2>();
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
