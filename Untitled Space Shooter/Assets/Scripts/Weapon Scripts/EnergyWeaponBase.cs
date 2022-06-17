using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class EnergyWeaponBase : MonoBehaviour
{
    public EnergyWeapons energyWeapons;
    public Recoil recoilScript;
    
    public Camera camera;
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private float currentHeatValue;
    [SerializeField]private bool firing;
    [SerializeField]private bool overheated;
    private float nextFire;
    
    [Header("Hipfrie Recoil")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;

    public Transform firePoint;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        camera = Camera.main;
    }

    void Start()
    {
        InputHandler.instance.inputActions.Player.Fire.performed += OnFire;
        InputHandler.instance.inputActions.Player.Fire.canceled += OnFire;
    }

    void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValueAsButton();
    }    
    void Update()
    {
        recoilScript.GunRotation(returnSpeed, snappiness);
        
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
        {
            firing = false;
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + energyWeapons.fireRate;
            
            recoilScript.RecoilFire(recoilX, recoilY, recoilZ);

            //Play sound here

            //Play muzzle flash here
            
            
            //Spawn Tracer
            var firePosition = firePoint.position;
            var tracer = Instantiate(energyWeapons.bulletTracer, firePosition, Quaternion.identity);
            tracer.AddPosition(firePosition);
            
            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, out hit, energyWeapons.maxDistance))return;

            tracer.transform.position = hit.point;
            var isDamageable = hit.collider.GetComponent<IDamageable>();

            if (hit.collider.GetComponent<IDamageable>() == null)return;
            
            
            if (hit.collider.CompareTag("Head"))
            {
                print("headshot");
                isDamageable.TakeDamage(energyWeapons.weaponDamage * energyWeapons.headshotMultiplier);
            }
            else
            {
                isDamageable.TakeDamage(energyWeapons.weaponDamage);
            }
            
        }
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
    
}
