using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private CapsuleCollider _collider;
    private PlayerInput _playerInput;

    public Vector3 movementInput;
    private Vector3 _movementDirection;
    private Vector3 _slopeMovementDirection;
    private RaycastHit _slopeHit;
    
    [SerializeField] private float gravityValue = -5f;
    public Vector3 playerVelocity = Vector3.zero;
    
    [Header("Speed Parameters")]
    [SerializeField] private bool useAcceleration;
    [SerializeField] private float airSpeed = 4f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float minSpeed;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    
    [Header("Jump Parameters")]
    [SerializeField] private float maxJumpHeight = 2.0f;
    [SerializeField] private float maxJumpTime = 0.5f;
    [SerializeField] private float initialJumpVelocity;
    [SerializeField] private bool isJumpPressed;

    [Header("Crouch Settings")] [Range(0, 1.0f)]
    [SerializeField] private float crouchSpeed = 1.0f;
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private bool isCrouching;
    
    [Header("Ground Detection Settings")]
    public LayerMask whatIsGround;
    public Transform checkSphere;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float rayLength = 0.4f;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private bool isHeadHitting;
    [SerializeField] private bool onSlope;
    
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
        _collider = GetComponent<CapsuleCollider>();
        SetupJump();

        for (int i = 0; i < 100; i++)
        {
            print(i % 2 == 0 ? $"{i} is an even number" : $"{i} is an odd number");
        }
        
    }
    
    private void Update()
    {
        groundedPlayer = IsGrounded();

        MovementInput();
        HandleGravity();
        HandleJump();
        //CrouchInputHandler();

        switch (groundedPlayer && !onSlope)
        {
            case true:
                _characterController.Move(_movementDirection * (currentSpeed * Time.deltaTime));
                break;
            case false:
                _characterController.Move(_movementDirection * (airSpeed * Time.deltaTime));
                break;
        }

        if (isCrouching)
        {
            AdjustHeight(_characterController.height);
        }

        if (PlayerInputManager.InputActions.Player.Crouch.WasPressedThisFrame())
        {
            if (!isCrouching)
            {
                isCrouching = true;
            }
            else if (isCrouching)
            {
                isCrouching = false;
            }
        }

        if (PlayerInputManager.InputActions.Player.Dash.WasPressedThisFrame())
        {
            StartCoroutine(Dash());
        }
        // if (PlayerInputManager.InputActions.Player.Crouch.WasPressedThisFrame())
        // {
        //     AdjustHeight(_characterController.height);
        // }
    }

    #endregion
    

    #region Private Methods
    
    private void DisableMovement()
    {
        PlayerInputManager.InputActions.Player.Move.Disable();
    }
    
    private void EnableMovement()
    {
        PlayerInputManager.InputActions.Player.Move.Enable();
    }

    private IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            _characterController.Move(_movementDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics.CheckSphere(checkSphere.position, sphereRadius, whatIsGround);
    }

    private void HandleGravity()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }
    
    private void MovementInput()
    {
        movementInput = PlayerInputManager.InputActions.Player.Move.ReadValue<Vector2>();
        _movementDirection = transform.right * movementInput.x + transform.forward * movementInput.y;

        if (!useAcceleration)
        {
            currentSpeed = maxSpeed;
            return;
        }
        if (movementInput != Vector3.zero) PlayerAcceleration();
        else currentSpeed = 0;
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
        if (PlayerInputManager.InputActions.Player.Jump.WasPressedThisFrame() && groundedPlayer)
        {
            playerVelocity.y = initialJumpVelocity;
        }

        if (isHeadHitting && !groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
    }
    
    private void SetupJump()
    {
        float timeToApex = maxJumpTime / 2;
        gravityValue = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    
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

    

    #endregion
}
