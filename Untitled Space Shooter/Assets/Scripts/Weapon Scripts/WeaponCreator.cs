using System;
using System.Collections;
using Core;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public enum WeaponTypes
{
    NormalWeapon,
    EnergyWeapon
}

public enum FireModes
{
    FullyAutomatic,
    SemiAutomatic,
    BurstFire
}
public class WeaponCreator : MonoBehaviour
{
    public WeaponTypes myWeaponType;
    public FireModes myFireModes;
    public BulletDecalPool bulletPool;
    public NormalWeapons weapons;
    public Recoil recoilScript;
    public PlayerLook playerLook;
    public Camera camera;

    [SerializeField]private AudioSource audioSource;
    [SerializeField]private Transform firePoint;
    
    [SerializeField]private bool firing;
    [SerializeField]private bool isAiming;
    [SerializeField]private bool isReloading;
    
    private static readonly int IsReloadingHash = Animator.StringToHash("isReloading");
    private float nextFire;
    private Animator anim;
    
    [Header("Normal Weapon Parameters")]
    [SerializeField]private int currentAmmoCount;
    [SerializeField]private int maxAmmoAmount;
    [SerializeField]private int reservesAmmoCount;
    [SerializeField]private GameObject AmmoHUD;
    [SerializeField]private GameObject AmmoPanel;
    [SerializeField]private TextMeshProUGUI AmmoCountCurrent;
    [SerializeField]private TextMeshProUGUI AmmoCountReserves;
    
    [Header("EnergyWeapon Parameters")]
    public bool overheated;
    public float currentHeatValue;
    [SerializeField]private GameObject EnergyWeaponPanel;
    [SerializeField]private Image CurrentHeatValue;
    public float fillSpeed = 2;
    public float target = 1;
    
    [Header("Burst Parameters")] 
    [SerializeField]private bool isBursting;
    [SerializeField]private int shotsPerBurst;
    [SerializeField]private float timeBetweenBullets;

    [Header("ADS Parameters")]
    [SerializeField]private float adsSpeed;
    [SerializeField]private float defaultFOV;
    [SerializeField]private float adsFOV;
    [SerializeField]private float adsRecoilX;
    [SerializeField]private float adsRecoilY;
    [SerializeField]private float adsRecoilZ;
    private Coroutine aimRoutine;
    
    [Header("Hip-fire Recoil Parameters")] 
    [SerializeField]private float recoilX;
    [SerializeField]private float recoilY;
    [SerializeField]private float recoilZ;
    
    [Header("Recoil Parameters")]
    [SerializeField]private float snappiness;
    [SerializeField]private float returnSpeed;

    private void OnEnable()
    {
        anim.Play("RifleEquipAnimation");
        AmmoHUD.SetActive(true);
    }
    
    private void OnDisable()
    {
        AmmoHUD.SetActive(false);
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
        
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
    }
    
    private void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }
    
    private void Update()
    {
        recoilScript.GunRotation(returnSpeed, snappiness);
        HandleAim();
        

        if (myWeaponType == WeaponTypes.NormalWeapon)
        {
            //Enable UI for this weapon type
            AmmoPanel.SetActive(true);
            EnergyWeaponPanel.SetActive(false);
            
            if (InputHandler.instance.inputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
                Reload();
            
            if (currentAmmoCount < 0 )
                currentAmmoCount = 0;
            
            switch (myFireModes)
            {
                case FireModes.FullyAutomatic:
                    if (firing && currentAmmoCount > 0 && !isReloading)
                        Shoot();
                    break;
                case FireModes.SemiAutomatic:
                    if (InputHandler.instance.inputActions.Player.Fire.WasPressedThisFrame() && currentAmmoCount > 0)
                        Shoot();
                    break;
                case FireModes.BurstFire:
                    if (InputHandler.instance.inputActions.Player.Fire.WasPressedThisFrame() && !isBursting && currentAmmoCount > 0)
                        StartCoroutine(BurstFire());
                    break;
            }
        }
        
        if (myWeaponType == WeaponTypes.EnergyWeapon)
        {
            EnergyWeaponPanel.SetActive(true);
            AmmoPanel.SetActive(false);
            UpdateHeatBar(currentHeatValue, weapons.maxHeatCapacity);

            if (myFireModes == FireModes.FullyAutomatic)
            {
                if (firing && !overheated)
                {
                    EnergyShoot();
                    WeaponOverHeat();
                }  
            }
            
            if (!firing)
                WeaponCooldown();
            if (overheated)
                firing = false;
        }
    }
    
    private void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + weapons.fireRate;

            currentAmmoCount--;
            
            UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);

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

    #region UI Update Methods
    
    private void UpdateAmmoCount(int currentAmmo, int reserveAmmo)
    {
        AmmoCountCurrent.text = currentAmmo.ToString();
        AmmoCountReserves.text = reserveAmmo.ToString();
    }
    
    private void UpdateHeatBar(float currentHeatValue, float maxHeatValue)
    {
        target = currentHeatValue / maxHeatValue;
        CurrentHeatValue.fillAmount = Mathf.MoveTowards(CurrentHeatValue.fillAmount, target, fillSpeed * Time.deltaTime);
    }
    
    #endregion

    #region ADS Methods
    
    private void HandleAim()
    {
        
        if (InputHandler.instance.inputActions.Player.Aim.WasPerformedThisFrame())
        {
            if (aimRoutine != null)
            {
                StopCoroutine(aimRoutine);
                aimRoutine = null;
            }
            aimRoutine = StartCoroutine(AimRoutine(true));
        }

        if (InputHandler.instance.inputActions.Player.Aim.WasReleasedThisFrame())
        {
            if (aimRoutine != null)
            {
                StopCoroutine(aimRoutine);
                aimRoutine = null;
            }
            aimRoutine = StartCoroutine(AimRoutine(false));
        }
    }

    private IEnumerator AimRoutine(bool isEnter)
    {
        float targetFOV = isEnter ? adsFOV : defaultFOV;
        float startingFOV = camera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < adsSpeed)
        {
            camera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / adsSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        camera.fieldOfView = targetFOV;
        aimRoutine = null;
    }
    

    #endregion
    
    #region Energy Weapon Methods
    
    private void EnergyShoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + weapons.fireRate;

            if (isAiming)
                recoilScript.RecoilFire(adsRecoilX, adsRecoilY, adsRecoilZ);
            else
                recoilScript.RecoilFire(recoilX, recoilY, recoilZ);

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
    
    private void WeaponOverHeat()
    {
        currentHeatValue += weapons.overheatSpeed * Time.deltaTime;

        if (currentHeatValue > weapons.maxHeatCapacity)
        {
            currentHeatValue = weapons.maxHeatCapacity;
        }

        if (currentHeatValue == weapons.maxHeatCapacity)
        {
            overheated = true;
        }
    }
    
    private void WeaponCooldown()
    {
        currentHeatValue -= weapons.overheatSpeed * Time.deltaTime;

        if (currentHeatValue <= weapons.minHeatThreshold)
        {
            overheated = false;
        }

        if (currentHeatValue < weapons.minHeatCapacity)
        {
            currentHeatValue = weapons.minHeatCapacity;
        }
    }

    #endregion

    #region Burst Weapon Methods
    
    private void BurstShoot()
    {
        if (currentAmmoCount == 0)
            return;
            
        
        currentAmmoCount--;
            
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);

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

    private IEnumerator BurstFire()
    {
        isBursting = true;
        for (int i = 0; i < shotsPerBurst; i++)
        {
            BurstShoot();
            yield return new WaitForSeconds(timeBetweenBullets);
            
        }
        yield return new WaitForSeconds(weapons.fireRate);
        isBursting = false;
    }

    #endregion

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
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
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
