using System;
using UnityEngine;

namespace Core
{
    public class EventManager:MonoBehaviour
    {
        public static event Action EnableNormalAmmoHUD; 
        public static event Action DisableNormalAmmoHUD;
        public static event Action EnableEnergyWeaponHUD; 
        public static event Action DisableEnergyWeaponHUD; 
        public static event Action DisableAllMovement;
        public static event Action EnableAllMovement;
        public static event Action DisableAllInput;
        public static event Action EnableAllInput;
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
        public static void OnDisableAllMovement()
        {
            DisableAllMovement?.Invoke();
        }
    
        public static void OnEnableAllMovement()
        {
            EnableAllMovement?.Invoke();
        }
    
        public static void OnEnableAllInput()
        {
            EnableAllInput?.Invoke();
        }
    
        public static void OnDisableAllInput()
        {
            DisableAllInput?.Invoke();
        }

        public static void OnEnableCursor()
        {
            EnableCursor?.Invoke();
        }

        public static void OnDisableCursor()
        {
            DisableCursor?.Invoke();
        }

        private static void OnEnableNormalAmmoHUD()
        {
            EnableNormalAmmoHUD?.Invoke();
        }

        private static void OnDisableNormalAmmoHUD()
        {
            DisableNormalAmmoHUD?.Invoke();
        }

        private static void OnEnableEnergyWeaponHUD()
        {
            EnableEnergyWeaponHUD?.Invoke();
        }

        private static void OnDisableEnergyWeaponHUD()
        {
            DisableEnergyWeaponHUD?.Invoke();
        }
    }
}
