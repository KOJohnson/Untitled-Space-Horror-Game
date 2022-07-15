using System;
using System.Collections;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    private PlayerInput playerInput;
    public Recoil recoilScript;
    
    private float currentCharge;
    [SerializeField]private float maxDistance;
    [SerializeField]private int chargeSpeed = 10;
    [SerializeField]private int minimumCharge = 20;
    [SerializeField]private float maxCharge = 100f;
    [SerializeField]private bool charging;
    
    [SerializeField] private float grenadeDamage;
    
    [Header("Hipfrie Recoil")] 
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;
    
    private Camera camera;
    
    RaycastHit hit;
    private RaycastHit lastHit;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip grenadeSFX;
    
    private void OnEnable()
    {
        playerInput.Enable();
    }
    
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Awake()
    {
        camera = Camera.main;
        playerInput = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.Player.Fire.started += _ => charging = true;
        playerInput.Player.Fire.canceled += _ => ChargeShoot();
    }

    // Update is called once per frame
    void Update()
    {
        recoilScript.GunRotation(returnSpeed, snappiness);
        
        if (charging)
        {
            ChargeValue();
            RayCast();
        }
        else currentCharge = 0f;
    }

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
        charging = false;
        
        if (currentCharge >= minimumCharge)
        {
            recoilScript.RecoilFire(recoilX, recoilY, recoilZ);
            print(currentCharge);
            FireProjectile();
        }
    }

    private void RayCast()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (!Physics.Raycast(ray, out hit, maxDistance))return;

        lastHit = hit;
    }

    private void FireProjectile()
    {
        print($"last hit object: {lastHit.collider.name}");
        //audioSource.PlayOneShot(grenadeSFX);
        var isDamageable = lastHit.collider.GetComponent<IDamageable>();

        if (lastHit.collider.GetComponent<IDamageable>() == null)return;
        isDamageable.TakeDamage(grenadeDamage);
    }
}
