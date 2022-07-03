using System;
using System.Collections;
using Core;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public enum WeaponTypes
{
    NormalWeapon,
    EnergyWeapon,
    ChargeWeapon
}
public class FullyAutomatic : MonoBehaviour
{
    public WeaponTypes myWeaponType;
    
    public BulletDecalPool bulletPool;
    public NormalWeapons weapons;
    public Recoil recoilScript;
    public Camera camera;

    [SerializeField]private AudioSource audioSource;
    
    [SerializeField]private bool firing;
    [SerializeField]private bool isAiming;
    [SerializeField]private bool isReloading;
    
    [SerializeField]private float currentAmmoCount;
    [SerializeField]private float maxAmmoAmount;
    [SerializeField]private float reservesAmmoCount;
    
    private float nextFire;
    private Animator anim;

    [Header("ADS Settings")]
    public float defaultFOV;
    public float adsFOV;
    public float adsSpeed;
    public float adsRecoilX;
    public float adsRecoilY;
    public float adsRecoilZ;
    
    [Header("Hipfire Recoil Settings")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;

    public Transform firePoint;
    private static readonly int IsReloadingHash = Animator.StringToHash("isReloading");
    
    private void OnEnable()
    {
        anim.Play("RifleEquipAnimation");
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        camera = Camera.main;

        InputHandler.instance.inputActions.Player.Fire.performed += OnFire;
        InputHandler.instance.inputActions.Player.Fire.canceled += OnFire;

        InputHandler.instance.inputActions.Player.Aim.performed += _ => isAiming = true;
        InputHandler.instance.inputActions.Player.Aim.canceled += _ => isAiming = false;
    }

    private void Start()
    {
        defaultFOV = camera.fieldOfView;
        anim.Play("RifleEquipAnimation");
    }
    
    private void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }

    // Update is called once per frame
    private void Update()
    {
        if (myWeaponType == WeaponTypes.NormalWeapon)
        {
            //Do Normal Shooting
        }
        
        if (myWeaponType == WeaponTypes.EnergyWeapon)
        {
            //Do laser Shooting
        }
        
        if (myWeaponType == WeaponTypes.ChargeWeapon)
        {
            //Do Charged Shooting
        }
        
        recoilScript.GunRotation(returnSpeed, snappiness);

        camera.fieldOfView = isAiming ? adsFOV : defaultFOV;

        if (firing && currentAmmoCount > 0 && !isReloading)
        {
            Shoot();
        }

        if (currentAmmoCount < 0 )
        {
            currentAmmoCount = 0;
        }
        
        if (InputHandler.instance.inputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
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

            if (isAiming)
            {
                recoilScript.RecoilFire(adsRecoilX, adsRecoilY, adsRecoilZ);
            }
            else
            {
                recoilScript.RecoilFire(recoilX, recoilY, recoilZ);
            }
           

            //Play sound here
            audioSource.PlayOneShot(weapons.fireSound);

            //Play muzzle flash here


            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, out hit, weapons.maxDistance))return;
            
            //Spawn Tracer
            TrailRenderer trailRenderer = Instantiate(weapons.bulletTracer, firePoint.position, quaternion.identity);
            StartCoroutine(SpawnTrail(trailRenderer, hit));
            
            //spawn decal
            GameObject decal = bulletPool.GetPooledObject();
            
            if (decal != null)
            {
                
                decal.transform.position = hit.point;
                decal.SetActive(true);
                decal.transform.rotation = Quaternion.LookRotation(hit.normal);
                
            }
            
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
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosisiton = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosisiton, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
    }
    
    private void Reload()
    {
        if (reservesAmmoCount <= 0)
        {
            reservesAmmoCount = 0;
            return;
        }
        
        anim.Play("BigRifle_Reload_01_Temp");
    }
    
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
        anim.SetBool(IsReloadingHash, true);
        isReloading = true;
    }
    
    private void NotReloading()
    {
        anim.SetBool(IsReloadingHash, false);
        isReloading = false;
    }

    #endregion
}
