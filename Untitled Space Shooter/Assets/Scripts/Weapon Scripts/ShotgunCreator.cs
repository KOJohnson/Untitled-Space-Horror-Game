using System;
using System.Collections;
using Core;
using Core.Interfaces;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using PlayerInputManager = Core.PlayerInputManager;
using Random = UnityEngine.Random;

public class ShotgunCreator : MonoBehaviour
{
    public BulletDecalPool bulletPool;
    public NormalWeapons weapons;
    public Recoil recoilScript;
    public ProceduralRecoil proceduralRecoil;
    public Camera camera;
    
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private Animator animator;
    [SerializeField]private Transform firePoint;
    
    [SerializeField]private bool firing;
    [SerializeField]private bool isAiming;
    [SerializeField]private bool isReloading;
    
    private float _nextFire;
    
    [Header("Ammo Parameters")]
    [SerializeField]private int currentAmmoCount;
    [SerializeField]private int maxAmmoAmount;
    [SerializeField]private int reservesAmmoCount;
    [SerializeField]private GameObject AmmoHUD;
    [SerializeField]private GameObject AmmoPanel;
    [SerializeField]private TextMeshProUGUI AmmoCountCurrent;
    [SerializeField] private TextMeshProUGUI AmmoCountReserves;

    [Header("Shotgun Parameters")] 
    [SerializeField]private AnimationClip equipAnimation;
    [SerializeField]private float pelletSpread;
    [SerializeField]private int pelletsPerShot;
    
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
        AmmoPanel.SetActive(true);
        animator.Play(equipAnimation.name);
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
    }

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();

        PlayerInputManager.InputActions.Player.Fire.performed += OnFire;
        PlayerInputManager.InputActions.Player.Fire.canceled += OnFire;
    }
    
    private void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        AmmoPanel.SetActive(true);
        
        proceduralRecoil.UpdatePositionRotation(snappiness, returnSpeed);
        
        if (PlayerInputManager.InputActions.Player.Reload.WasPressedThisFrame() && currentAmmoCount < maxAmmoAmount)
            Reload();
            
        if (currentAmmoCount < 0 )
            currentAmmoCount = 0;
        
        if (PlayerInputManager.InputActions.Player.Fire.WasPressedThisFrame() && currentAmmoCount > 0)
            Shoot();
        
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + weapons.fireRate;

            currentAmmoCount--;
            
            UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
            
            proceduralRecoil.Recoil(recoilX, recoilY, recoilZ, kickBackZ);
            
            SoundManager.Instance.PlayAudio(audioSource,weapons.fireSound);
            //audioSource.PlayOneShot(weapons.fireSound);

            for (int i = 0; i < pelletsPerShot; i++)
            {
                RaycastHit hit;
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (!Physics.Raycast(camera.transform.position, GetShootingDirection(), out hit, weapons.maxDistance))return;
            
                //Spawn Tracer
                TrailRenderer trailRenderer = Instantiate(weapons.bulletTracer, firePoint.position, quaternion.identity);
                StartCoroutine(SpawnTrail(trailRenderer, hit));

                var isDamageable = hit.collider.GetComponent<IDamageable>();

                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    if (hit.collider.CompareTag("Head"))
                        isDamageable.TakeDamage(weapons.weaponDamage * weapons.headshotMultiplier);
                    else
                        isDamageable.TakeDamage(weapons.weaponDamage);
                }
            }
        }
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 targetPos = camera.transform.position + camera.transform.forward * weapons.maxDistance;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-pelletSpread, pelletSpread),
            targetPos.y + Random.Range(-pelletSpread, pelletSpread),
            targetPos.z + Random.Range(-pelletSpread, pelletSpread)
        );
        
        Vector3 direction = targetPos - camera.transform.position;
        return direction.normalized;
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
        
        if (reservesAmmoCount <= 0)
        {
            reservesAmmoCount = 0;
        }
        
        UpdateAmmoCount(currentAmmoCount, reservesAmmoCount);
    }
    
    #region UI Update Methods
    
    private void UpdateAmmoCount(int currentAmmo, int reserveAmmo)
    {
        AmmoCountCurrent.text = currentAmmo.ToString();
        AmmoCountReserves.text = reserveAmmo.ToString();
    }

    #endregion
    
}
