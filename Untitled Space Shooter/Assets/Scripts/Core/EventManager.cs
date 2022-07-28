using System;
using UnityEngine;

namespace Core
{
    public class EventManager:MonoBehaviour
    {
        public static event Action ToggleNormalAmmoHUD; 
        public static event Action DisableNormalAmmoHUD;
        public static event Action ToggleEnergyWeaponHUD; 
        public static event Action DisableEnergyWeaponHUD; 
        public static event Action DisableAllMovement;
        public static event Action EnableAllMovement;
        public static event Action DisablePlayerInput;
        public static event Action EnablePlayerInput;
        public static event Action EnableCursor;
        public static event Action DisableCursor;
        public static event Action EnableCrosshair;
        public static event Action DisableCrosshair;

        public static void OnEnableCrosshair()
        {
            EnableCrosshair?.Invoke();
        }
        
        public static void OnDisableCrosshair()
        {
            DisableCrosshair?.Invoke();
        }

        public static void OnEnablePlayerInput()
        {
            EnablePlayerInput?.Invoke();
        }
    
        public static void OnDisablePlayerInput()
        {
            DisablePlayerInput?.Invoke();
        }
        
        public static void OnDisableAllMovement()
        {
            DisableAllMovement?.Invoke();
        }
    
        public static void OnEnableAllMovement()
        {
            EnableAllMovement?.Invoke();
        }

        public static void OnEnableCursor()
        {
            EnableCursor?.Invoke();
        }

        public static void OnDisableCursor()
        {
            DisableCursor?.Invoke();
        }

        public static void OnToggleNormalAmmoHUD()
        {
            ToggleNormalAmmoHUD?.Invoke();
        }

        private static void OnDisableNormalAmmoHUD()
        {
            DisableNormalAmmoHUD?.Invoke();
        }

        public static void OnToggleEnergyWeaponHUD()
        {
            ToggleEnergyWeaponHUD?.Invoke();
        }

        private static void OnDisableEnergyWeaponHUD()
        {
            DisableEnergyWeaponHUD?.Invoke();
        }
    }
}
