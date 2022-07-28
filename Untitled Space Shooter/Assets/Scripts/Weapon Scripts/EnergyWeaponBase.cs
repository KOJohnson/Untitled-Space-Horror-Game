using System;
using System.Collections;
using Core;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using PlayerInputManager = Core.PlayerInputManager;

[RequireComponent(typeof(AudioSource))]
public class EnergyWeaponBase : MonoBehaviour
{
    public EnergyWeapons energyWeapons;
    public ProceduralRecoil proceduralRecoil;
    public BulletTrailPool trailPool;
    private Camera _camera;
    
    private Animator _anim;
    private float _nextFire;
    
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private bool firing;
    
    [Header("EnergyWeapon Parameters")]
    public bool overheated;
    public float currentHeatValue;
    
    [Header("EnergyWeapon UI")]
    [SerializeField]private GameObject EnergyWeaponPanel;
    [SerializeField]private Image CurrentHeatValue;
    public float fillSpeed = 2;
    public float target = 1;
    
    
    [Header("ADS Parameters")] 
    [SerializeField]private bool isAiming;
    [SerializeField]private bool canAim;
    [SerializeField]private float adsSpeed;
    [SerializeField]private float defaultFOV;
    [SerializeField]private float adsFOV;
    [SerializeField]private float adsRecoilX;
    [SerializeField]private float adsRecoilY;
    [SerializeField]private float adsRecoilZ;
    private Coroutine _aimRoutine;
    
    [Header("Hipfrie Recoil")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    [SerializeField]private float kickBackZ;

    [Header("Recoil Parameters")]
    [SerializeField]private float snappiness;
    [SerializeField]private float returnSpeed;

    public Transform firePoint;

    private void OnEnable()
    {
        _anim.Play("RifleEquipAnimation");
        EnergyWeaponPanel.SetActive(true);
    }

    private void OnDisable()
    {
        EnergyWeaponPanel.SetActive(false);
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        trailPool = GetComponent<BulletTrailPool>();
        _camera = Camera.main;
        
        PlayerInputManager.InputActions.Player.Fire.performed += OnFire;
        PlayerInputManager.InputActions.Player.Fire.canceled += OnFire;
        
        PlayerInputManager.InputActions.Player.Aim.performed += _ => isAiming = true;
        PlayerInputManager.InputActions.Player.Aim.canceled += _ => isAiming = false;
    }

    void Start()
    {
        defaultFOV = _camera.fieldOfView;
        _anim.Play("RifleEquipAnimation");
        
        UpdateHeatBar(currentHeatValue, energyWeapons.maxHeatCapacity);
        
    }

    void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }    
    void Update()
    {
        proceduralRecoil.UpdatePositionRotation(snappiness, returnSpeed);
        UpdateHeatBar(currentHeatValue, energyWeapons.maxHeatCapacity);
        
        if (canAim)
        {
            HandleAim();
        }

        if (firing && !overheated)
        {
            Shoot();
            WeaponOverHeat();
        }
        else if (!firing)
        {
            WeaponCooldown();
        }

        if (overheated)
            firing = false;
        
    }

    void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + energyWeapons.fireRate;
            
            if (isAiming)
            {
                proceduralRecoil.Recoil(adsRecoilX, adsRecoilY,adsRecoilZ, kickBackZ);
            }
            else
            {
                proceduralRecoil.Recoil(recoilX, recoilY,recoilZ, kickBackZ);
            }

            //Play sound here
            SoundManager.Instance.PlayAudio(audioSource,energyWeapons.fireSound);

            //Play muzzle flash here

            RaycastHit hit;
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, out hit, energyWeapons.maxDistance))return;
            
            //Spawn Tracer
            TrailRenderer trail = trailPool.GetPooledObject();
            if (trail != null)
            {
                trail.transform.position = firePoint.position;
                trail.transform.rotation = Quaternion.identity;
                trail.gameObject.SetActive(true);
                StartCoroutine(SpawnTrail(trail, hit));
            }
            
            var isDamageable = hit.collider.GetComponent<IDamageable>();

            if (hit.collider.GetComponent<IDamageable>() == null)return;

            if (hit.collider.CompareTag("Head"))
            {
                isDamageable.TakeDamage(energyWeapons.weaponDamage * energyWeapons.headshotMultiplier);
            }
            else
            {
                isDamageable.TakeDamage(energyWeapons.weaponDamage);
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
    
    void WeaponOverHeat()
    {
        currentHeatValue += energyWeapons.overheatSpeed * Time.deltaTime;

        if (currentHeatValue > energyWeapons.maxHeatCapacity)
        {
            currentHeatValue = energyWeapons.maxHeatCapacity;
        }

        if (currentHeatValue == energyWeapons.maxHeatCapacity)
        {
            overheated = true;
        }
    }
    
    void WeaponCooldown()
    {
        currentHeatValue -= energyWeapons.overheatSpeed * Time.deltaTime;

        if (currentHeatValue <= energyWeapons.minHeatThreshold)
        {
            overheated = false;
        }

        if (currentHeatValue < energyWeapons.minHeatCapacity)
        {
            currentHeatValue = energyWeapons.minHeatCapacity;
        }
    }
    
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
    
    #region UI Update Methods

    private void UpdateHeatBar(float currentHeatValue, float maxHeatValue)
    {
        target = currentHeatValue / maxHeatValue;
        CurrentHeatValue.fillAmount = Mathf.MoveTowards(CurrentHeatValue.fillAmount, target, fillSpeed * Time.deltaTime);
    }
    
    #endregion
    
}
