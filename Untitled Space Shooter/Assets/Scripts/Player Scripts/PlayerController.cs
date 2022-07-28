using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private PlayerInput _playerInput;
    private Camera _camera;

    private Vector3 _movementInput;
    private Vector3 _movementDirection;
    private Vector3 _slopeMovementDirection;

    private float _nextFire;

    [SerializeField] private float gravityValue = -40f;
    [SerializeField] private float downForce = -100f;
    private Vector3 _playerVelocity = Vector3.zero;
    
    [Header("Speed Parameters")]
    [SerializeField] private bool useAcceleration;
    [SerializeField] private float airSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;

    [Header("Dash Parameters")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashRate;

    [Header("Slope Parameters")] 
    [SerializeField]private float maxSlopeAngle;
    private RaycastHit _slopeHit;

    [Header("Jump Parameters")] 
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private float maxJumpHeight = 2.0f;
    [SerializeField] private float maxJumpTime = 0.5f;
    [SerializeField] private float initialJumpVelocity = 12f;
    [SerializeField] private bool isJumpPressed;

    [Header("Crouch Settings")] [Range(0, 1.0f)]
    [SerializeField] private float crouchSpeed = 1.0f;
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private bool isCrouching;
    
    [Header("Ground Detection Settings")]
    public LayerMask whatIsGround;
    public Transform checkSphere;
    public Transform headCheck;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float rayLength = 0.4f;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private bool onGroundLastFrame = false;
    [SerializeField] private bool isHeadHitting;
    [SerializeField] private bool onSlope;
    
    [Header("Footstep Settings")]
    [Range(0, 10)]public float minimumSpeedThreshold;
    [Range(-100,100)] public float minLandThreshhold;
    [Range(0,1)][SerializeField]private float fireRate = 0.9f;
    [SerializeField]private AudioClip[] metalFootSteps;
    [SerializeField]private AudioClip[] metalJump;
    [SerializeField]private AudioClip[] metalLand;
    private float _nextFootstepFire;

    [Header("Player Voice")] 
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    [Header("Audio Settings")] 
    public AudioSource audioSource;
    
    
    #region MonoBehaviour CallBacks
    
    private void OnEnable()
    {
        EventManager.DisableAllMovement += DisableMovement;
        EventManager.EnableAllMovement += EnableMovement;
    }

    private void OnDisable()
    {
        EventManager.DisableAllMovement -= DisableMovement;
        EventManager.EnableAllMovement -= EnableMovement;
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _camera = Camera.main;
        //SetupJump();
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {
        groundedPlayer = IsGrounded();
        onSlope = OnSlope();
        isHeadHitting = IsHeadHitting();
        
        MovementInput();
        HandleGravity();
        HandleJump();
        MovePlayer();
        HandleFootsteps();

        if (IsHeadHitting())
        {
            Debug.DrawRay(headCheck.position, Vector3.up * rayLength, Color.green);
        }
        else
        {
            Debug.DrawRay(headCheck.position, Vector3.up * rayLength, Color.red);
        }

        if (PlayerInputManager.InputActions.Player.Dash.WasPressedThisFrame())
        {
            StartCoroutine(Dash());
        }

        if (groundedPlayer && !onGroundLastFrame)
        {
            SoundManager.Instance.PlayAudio(audioSource,metalLand[Random.Range(0, metalLand.Length -1)]);
        }
        onGroundLastFrame = groundedPlayer;
    }

    private void MovePlayer()
    {
        currentSpeed = IsGrounded() ? runSpeed : airSpeed;
        
        if (IsGrounded())
        {
            _characterController.Move(_movementDirection * (currentSpeed * Time.deltaTime));
        }
        else if (!IsGrounded())
        {
            _characterController.Move(_movementDirection * (airSpeed * Time.deltaTime));
        }
        
        if (!useAcceleration)
        {
            currentSpeed = maxSpeed;
            return;
        }
        if (_movementInput != Vector3.zero) PlayerAcceleration();
        else currentSpeed = 0;
    }

    private void HandleFootsteps()
    {
        var playerVelocity = _characterController.velocity.magnitude;
        
        if (groundedPlayer && playerVelocity >= minimumSpeedThreshold && Time.time > _nextFootstepFire) 
        {
            _nextFootstepFire = Time.time + fireRate;
            SoundManager.Instance.PlayAudio(audioSource,metalFootSteps[Random.Range(0, metalFootSteps.Length -1)]);
        }
    }

    #endregion
    

    #region Private Methods
    private IEnumerator Dash()
    {
        if (_movementDirection == Vector3.zero)
            yield break;
        
        float startTime = Time.time;

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + dashRate;

            while (Time.time < startTime + dashTime)
            {
                _characterController.Move(_movementDirection * (dashSpeed * Time.deltaTime));
                yield return null;
            }
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _characterController.height * 0.5f + 0.3f, whatIsGround))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(checkSphere.position, sphereRadius, whatIsGround);
    }

    private bool IsHeadHitting()
    {
        return Physics.CheckSphere(headCheck.position, rayLength, whatIsGround);
    }
    
    private Vector3 GetSlopeMovementDirection()
    {
        return Vector3.ProjectOnPlane(_movementDirection, _slopeHit.normal).normalized;
    }

    private void HandleGravity()
    {
        if (groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        
        
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
    
    private void MovementInput()
    {
        _movementInput = PlayerInputManager.Instance.PlayerMovementInput();
        _movementDirection = transform.right * _movementInput.x + transform.forward * _movementInput.y;
    }

    private void AdjustHeight(float height)
    {
        float center = height / 2;
    
        _characterController.height = Mathf.Lerp(_characterController.height, height, crouchSpeed);
        _characterController.center = Vector3.Lerp(_characterController.center, new Vector3(0, center, 0), crouchSpeed);
        
        _collider.height = Mathf.Lerp(_collider.height, height, crouchSpeed);
        _collider.center = Vector3.Lerp(_collider.center, new Vector3(0, center, 0), crouchSpeed);
    }

    private void HandleJump()
    {
        if (jumpCount > 0 && PlayerInputManager.InputActions.Player.Jump.WasPressedThisFrame() && groundedPlayer)
        {
            jumpCount = 1;
            SoundManager.Instance.PlayAudio(audioSource, jumpSound);
            SoundManager.Instance.PlayAudio(audioSource,metalJump[Random.Range(0, metalJump.Length -1)]);
            _playerVelocity.y = initialJumpVelocity;
        }
        
        if (isHeadHitting && !groundedPlayer)
        {
            _playerVelocity.y = downForce;
        }
    }
    
    // private void SetupJump()
    // {
    //     float timeToApex = maxJumpTime / 2;
    //     //gravityValue = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
    //     initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    // }
    
    private void CrouchInputHandler()
    {
        switch (isCrouching)
        {
            case false:
                isCrouching = true;
                break;
            case true:
                isCrouching = false;
                break;
        }
    }

    private void PlayerAcceleration()
    {
        currentSpeed += acceleration * Time.deltaTime;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
    }
    
    private void PlayerDeceleration()
    {
        currentSpeed -= deceleration * Time.deltaTime;
        if (currentSpeed < minSpeed) currentSpeed = minSpeed;
    }

    #endregion


    #region Event Calls
    
    public void AddForce(float force)
    {
        _playerVelocity.y = force;
        SoundManager.Instance.PlayAudio(audioSource, jumpSound);
    }
    
    private void DisableMovement()
    {
        PlayerInputManager.InputActions.Player.Move.Disable();
    }
    
    private void EnableMovement()
    {
        PlayerInputManager.InputActions.Player.Move.Enable();
    }
    
    #endregion
}
