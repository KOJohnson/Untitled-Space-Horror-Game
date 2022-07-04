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
    [SerializeField]private Transform firePoint;
    
    [SerializeField]private bool firing;
    [SerializeField]private bool isAiming;
    [SerializeField]private bool isReloading;
    
    private static readonly int IsReloadingHash = Animator.StringToHash("isReloading");
    private float nextFire;
    private Animator anim;
    
    [Header("Normal Weapon Settings")]
    [SerializeField]private int currentAmmoCount;
    [SerializeField]private int maxAmmoAmount;
    [SerializeField]private int reservesAmmoCount;
    [SerializeField]private GameObject AmmoPanel;
    [SerializeField]private TextMeshProUGUI AmmoCountCurrent;
    [SerializeField]private TextMeshProUGUI AmmoCountReserves;
    
    [Header("EnergyWeapon Settings")]
    public bool overheated;
    public float currentHeatValue;
    [SerializeField]private GameObject EnergyWeaponPanel;
    [SerializeField]private Image CurrentHeatValue;
    public float fillSpeed = 2;
    public float target = 1;
    
    [Header("ChargeWeapon Settings")] 
    public bool isCharging;
    public float minCharge;
    public float maxCharge;
    public float chargeSpeed;
    public float currentCharge;
    private RaycastHit lastHit;

    [Header("ADS Settings")]
    public float defaultFOV;
    public float adsFOV;
    public float adsSpeed;
    public float adsRecoilX;
    public float adsRecoilY;
    public float adsRecoilZ;
    
    [Header("Hip-fire Recoil Settings")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    
    [Header("Recoil Settings")]
    public float snappiness;
    public float returnSpeed;

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

        //InputHandler.instance.inputActions.Player.Fire.started += _ => isCharging = true;
        //InputHandler.instance.inputActions.Player.Fire.canceled += _ => ChargeShoot();

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
        camera.fieldOfView = isAiming ? adsFOV : defaultFOV;
        
        if (myWeaponType == WeaponTypes.NormalWeapon)
        {
            AmmoPanel.SetActive(true);
            EnergyWeaponPanel.SetActive(false);
            //enable UI for this weapon type
            
            if (firing && currentAmmoCount > 0 && !isReloading)
                Shoot();

            if (currentAmmoCount < 0 )
                currentAmmoCount = 0;

            if (InputHandler.instance.inputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
                Reload();
        }
        
        if (myWeaponType == WeaponTypes.EnergyWeapon)
        {
            EnergyWeaponPanel.SetActive(true);
            AmmoPanel.SetActive(false);
            
            //enable UI for this weapon type
            UpdateHeatBar(currentHeatValue, weapons.maxHeatCapacity);
            
            if (firing && !overheated)
            {
                EnergyShoot();
                WeaponOverHeat();
            }
            else if (!firing)
            {
                WeaponCooldown();
            }
            if (overheated)
                firing = false;
        }
        
        if (myWeaponType == WeaponTypes.ChargeWeapon)
        {
            EnergyWeaponPanel.SetActive(false);
            AmmoPanel.SetActive(false);
            //enable UI for this weapon type
            
            if (isCharging)
            {
                ChargeValue();
                RayCast();
            }
            else currentCharge = 0f;
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
    
    private void Reload()
    {
        if (reservesAmmoCount <= 0)
        {
            reservesAmmoCount = 0;
            return;
        }
        
        anim.Play("BigRifle_Reload_01_Temp");
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

    #region ChargingWeapon
    
    private void ChargeValue()
    {
        currentCharge += chargeSpeed * Time.deltaTime;

        if (currentCharge > maxCharge)
        {
            currentCharge = maxCharge;
        }
    }
    private void ChargeShoot()
    {
        isCharging = false;
        
        if (currentCharge >= minCharge)
        {
            recoilScript.RecoilFire(recoilX, recoilY, recoilZ);
            print(currentCharge);
            FireProjectile();
        }
    }

    private void RayCast()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (!Physics.Raycast(ray, out RaycastHit hit, weapons.maxDistance))return;

        lastHit = hit;
    }

    private void FireProjectile()
    {
        print($"last hit object: {lastHit.collider.name}");
        
        var isDamageable = lastHit.collider.GetComponent<IDamageable>();

        if (lastHit.collider.GetComponent<IDamageable>() == null)return;
        isDamageable.TakeDamage(weapons.chargeDamage);
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
