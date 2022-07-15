using UnityEngine;

namespace Core
{
    public class UiManager : MonoBehaviour
    {
        public GameObject normalWeaponHUD;
        public GameObject energyWeaponHUD;

        private void OnEnable()
        {
            EventManager.ToggleNormalAmmoHUD += ToggleNormalWeaponHud;
            EventManager.ToggleEnergyWeaponHUD += ToggleEnergyWeaponHud;
        }

        private void OnDisable()
        {
            EventManager.ToggleNormalAmmoHUD -= ToggleNormalWeaponHud;
            EventManager.ToggleNormalAmmoHUD -= ToggleEnergyWeaponHud;
        }

        private void ToggleNormalWeaponHud()
        {
            if (normalWeaponHUD == enabled)
            {
                normalWeaponHUD.SetActive(false);
            }
            else
            {
                normalWeaponHUD.SetActive(true);
            }
            normalWeaponHUD.SetActive(normalWeaponHUD != enabled);
            Debug.Log("Toggle Normal Weapon HUD");
        }

        private void ToggleEnergyWeaponHud()
        {
            energyWeaponHUD.SetActive(!energyWeaponHUD);
            Debug.Log("Toggle Energy Weapon HUD");
        }
    }
}
