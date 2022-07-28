using System.Collections;
using Core;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = Core.PlayerInputManager;

[RequireComponent(typeof(AudioSource))]
public class SemiAutomaticGunBase : MonoBehaviour
{
    public NormalWeapons weapons;
    public Recoil recoilScript;
    public Camera camera;
    
    [SerializeField]private AudioSource audioSource;
    
    [SerializeField]private bool isFiring;
    [SerializeField]private bool isReloading;
    
    [SerializeField]private float currentAmmoCount;
    [SerializeField]private float maxAmmoAmount;
    [SerializeField]private float reservesAmmoCount;
    
    private float nextFire;
    private Animator anim;
    
    [Header("Hipfrie Recoil")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;

    public Transform firePoint;
    private static readonly int IsReloading = Animator.StringToHash("isReloading");

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void Update()
    {
        recoilScript.GunRotation(returnSpeed, snappiness);
        
        if (isFiring && currentAmmoCount > 0 && !isReloading)
        {
            Shoot();
        }

        if (currentAmmoCount < 0 )
        {
            currentAmmoCount = 0;
        }

        if ( PlayerInputManager.InputActions.Player.Fire.WasPressedThisFrame())
        {
            isFiring = true;
        }
        
        if ( PlayerInputManager.InputActions.Player.Fire.WasReleasedThisFrame())
        {
            isFiring = false;
        }
        
        if ( PlayerInputManager.InputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + weapons.fireRate;

            currentAmmoCount--;
            recoilScript.RecoilFire(recoilX, recoilY, recoilZ);

            //Play sound here

            //Play muzzle flash here

            //Spawn Tracer
            
            
            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, out hit, weapons.maxDistance))return;
            
            var isDamageable = hit.collider.GetComponent<IDamageable>();

            if (hit.collider.GetComponent<IDamageable>() == null)return;
            
            
            if (hit.collider.CompareTag("Head"))
            {
                isDamageable.TakeDamage(weapons.weaponDamage * weapons.headshotMultiplier);
            }
            else
            {
                isDamageable.TakeDamage(weapons.weaponDamage);
            }
            
        }
        
        isFiring = false;
    }

    private void Reload()
    {
        if (reservesAmmoCount <= 0)
        {
            reservesAmmoCount = 0;
            return;
        }
        
        anim.Play("testReload");
    }

    //Animation Events later for weapon reloading 

    #region Animation Events

    private void AddAmmo()
    {
        var desiredAmmoToAdd = maxAmmoAmount - currentAmmoCount;

        if (reservesAmmoCount < desiredAmmoToAdd)
        {
            currentAmmoCount += reservesAmmoCount;
            reservesAmmoCount -= reservesAmmoCount;
        }
        else
        {
            reservesAmmoCount -= desiredAmmoToAdd;
            currentAmmoCount += desiredAmmoToAdd;
        }

        //isReloading = false;
    }
    
    private void Reloading()
    {
        anim.SetBool(IsReloading, true);
        isReloading = true;
    }
    
    private void NotReloading()
    {
        anim.SetBool("isReloading", false);
        isReloading = false;
    }

    #endregion

    
}
