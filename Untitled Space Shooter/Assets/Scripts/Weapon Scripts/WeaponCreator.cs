using System;
using System.Collections;
using Core;
using Core.Interfaces;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using PlayerInputManager = Core.PlayerInputManager;

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
    public BulletTrailPool trailPool;
    public BulletDecalPool bulletPool;
    public NormalWeapons weapons;
    public Recoil recoilScript;
    public ProceduralRecoil proceduralRecoil;
    private Camera _camera;

    [SerializeField]private AudioSource audioSource;
    [SerializeField]private Transform firePoint;
    
    [SerializeField]private bool firing;
    [SerializeField]private bool isAiming;
    [SerializeField]private bool isReloading;
    
    private static readonly int IsReloadingHash = Animator.StringToHash("isReloading");
    private float _nextFire;
    private Animator _anim;
    
    [Header("Normal Weapon Parameters")]
    [SerializeField]private int currentAmmoCount;
    [SerializeField]private int maxAmmoAmount;
    [SerializeField]private int reservesAmmoCount;
    [SerializeField]private GameObject AmmoHUD;
    [SerializeField]private GameObject AmmoPanel;
    [SerializeField]private TextMeshProUGUI AmmoCountCurrent;
    [SerializeField]private TextMeshProUGUI AmmoCountReserves;

    [Header("Burst Parameters")] 
    [SerializeField]private bool isBursting;
    [SerializeField]private int shotsPerBurst;
    [SerializeField]private float timeBetweenBullets;

    [Header("ADS Parameters")] 
    [SerializeField]private bool canAim;
    [SerializeField]private float adsSpeed;
    [SerializeField]private float defaultFOV;
    [SerializeField]private float adsFOV;
    [SerializeField]private float adsRecoilX;
    [SerializeField]private float adsRecoilY;
    [SerializeField]private float adsRecoilZ;
    private Coroutine _aimRoutine;
    
    [Header("Hip-fire Recoil Parameters")] 
    [SerializeField]private float recoilX;
    [SerializeField]private float recoilY;
    [SerializeField]private float recoilZ;
    [SerializeField]private float kickBackZ;
    
    [Header("Recoil Parameters")]
    [SerializeField]private float snappiness;
    [SerializeField]private float returnSpeed;

    private void OnEnable()
    {
        //_anim.Play("RifleEquipAnimation");
        AmmoPanel.SetActive(true);
        UpdateAmmoCount(currentAmmoCount,reservesAmmoCount);
    }
    
    private void OnDisable()
    {
        AmmoPanel.SetActive(false);
        //_anim.keepAnimatorControllerStateOnDisable = true;
        isBursting = false;
        isReloading = false;
    }

    private void Awake()
    {
        //_anim = GetComponent<Animator>();
        trailPool = GetComponent<BulletTrailPool>();
        _camera = Camera.main;

        PlayerInputManager.InputActions.Player.Fire.performed += OnFire;
        PlayerInputManager.InputActions.Player.Fire.canceled += OnFire;

        PlayerInputManager.InputActions.Player.Aim.performed += _ => isAiming = true;
        PlayerInputManager.InputActions.Player.Aim.canceled += _ => isAiming = false;
    }

    private void Start()
    {
        defaultFOV = _camera.fieldOfView;
        //_anim.Play("RifleEquipAnimation");
        
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
    }
    
    private void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }
    
    private void Update()
    {
        //recoilScript.GunRotation(returnSpeed, snappiness);
        proceduralRecoil.UpdatePositionRotation(snappiness, returnSpeed);
        
        if (canAim)
        {
            HandleAim();
        }
        
        if (myWeaponType == WeaponTypes.NormalWeapon)
        {
            //Enable UI for this weapon type
            AmmoPanel.SetActive(true);

            if (PlayerInputManager.InputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
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
                    if (PlayerInputManager.InputActions.Player.Fire.WasPressedThisFrame() && currentAmmoCount > 0)
                        Shoot();
                    break;
                case FireModes.BurstFire:
                    if (PlayerInputManager.InputActions.Player.Fire.WasPressedThisFrame() && !isBursting && currentAmmoCount > 0)
                        StartCoroutine(BurstFire());
                    break;
            }
        }
    }
    
    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + weapons.fireRate;

            currentAmmoCount--;
            
            UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
            
            if (isAiming)
            {
                proceduralRecoil.Recoil(adsRecoilX, adsRecoilY,adsRecoilZ, kickBackZ);
            }
            else
            {
                proceduralRecoil.Recoil(recoilX, recoilY,recoilZ, kickBackZ);
            }

            //Play sound here
            SoundManager.Instance.PlayAudio(audioSource,weapons.fireSound);

            //Play muzzle flash here


            RaycastHit hit;
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, out hit, weapons.maxDistance))return;

            TrailRenderer trail = trailPool.GetPooledObject();
            if (trail != null)
            {
                trail.transform.position = firePoint.position;
                trail.transform.rotation = Quaternion.identity;
                trail.gameObject.SetActive(true);
                StartCoroutine(SpawnTrail(trail, hit));
            }
            
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
        trail.gameObject.SetActive(false);
    }
    
    private void Reload()
    {
        if (reservesAmmoCount <= 0)
        {
            reservesAmmoCount = 0;
            return;
        }
        
        //_anim.Play("BigRifle_Reload_01_Temp");
    }

    #region UI Update Methods
    
    private void UpdateAmmoCount(int currentAmmo, int reserveAmmo)
    {
        AmmoCountCurrent.text = currentAmmo.ToString();
        AmmoCountReserves.text = reserveAmmo.ToString();
    }

    #endregion

    #region ADS Methods
    
    private void HandleAim()
    {
        
        if (PlayerInputManager.InputActions.Player.Aim.WasPerformedThisFrame())
        {
            if (_aimRoutine != null)
            {
                StopCoroutine(_aimRoutine);
                _aimRoutine = null;
            }
            _aimRoutine = StartCoroutine(AimRoutine(true));
        }

        if (PlayerInputManager.InputActions.Player.Aim.WasReleasedThisFrame())
        {
            if (_aimRoutine != null)
            {
                StopCoroutine(_aimRoutine);
                _aimRoutine = null;
            }
            _aimRoutine = StartCoroutine(AimRoutine(false));
        }
    }

    private IEnumerator AimRoutine(bool isEnter)
    {
        float targetFOV = isEnter ? adsFOV : defaultFOV;
        float startingFOV = _camera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < adsSpeed)
        {
            _camera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / adsSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _camera.fieldOfView = targetFOV;
        _aimRoutine = null;
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
            proceduralRecoil.Recoil(adsRecoilX, adsRecoilY,adsRecoilZ, kickBackZ);
        }
        else
        {
            proceduralRecoil.Recoil(recoilX, recoilY,recoilZ, kickBackZ);
        }
       

        //Play sound here
        SoundManager.Instance.PlayAudio(audioSource,weapons.fireSound);

        //Play muzzle flash here


        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
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
        _anim.SetBool(IsReloadingHash, true);
        isReloading = true;
    }
    
    private void NotReloading()
    {
        _anim.SetBool(IsReloadingHash, false);
        isReloading = false;
    }

    #endregion
}
