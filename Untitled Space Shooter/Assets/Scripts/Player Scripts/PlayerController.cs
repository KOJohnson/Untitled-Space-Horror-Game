using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private PlayerInput _playerInput;

    private Vector3 _movementInput;
    private Vector3 _movementDirection;
    private Vector3 _slopeMovementDirection;

    private float _nextFire;

    [SerializeField] private float gravityValue = -40f;
    [SerializeField] private float testGravityValue = -100f;
    private Vector3 _playerVelocity = Vector3.zero;
    
    [Header("Speed Parameters")]
    [SerializeField] private bool useAcceleration;
    [SerializeField] private float airSpeed = 4f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed = 10f;
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
    public Transform headCheck;
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
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        SetupJump();
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

        if (IsHeadHitting())
        {
            Debug.DrawRay(headCheck.position, Vector3.up * rayLength, Color.green);
        }
        else
        {
            Debug.DrawRay(headCheck.position, Vector3.up * rayLength, Color.red);
        }
        
        if (PlayerInputManager.InputActions.Player.Crouch.WasPressedThisFrame())
        {
            if (!isCrouching)
            {
                isCrouching = true;
                AdjustHeight(_characterController.height);
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
    }

    private void MovePlayer()
    {
        if (IsGrounded())
        {
            _characterController.Move(_movementDirection * (currentSpeed * Time.deltaTime));
        }
        else if (!IsGrounded())
        {
            _characterController.Move(_movementDirection * (airSpeed * Time.deltaTime));
        }
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
        _movementInput = PlayerInputManager.InputActions.Player.Move.ReadValue<Vector2>();
        _movementDirection = transform.right * _movementInput.x + transform.forward * _movementInput.y;

        if (!useAcceleration)
        {
            currentSpeed = maxSpeed;
            return;
        }
        if (_movementInput != Vector3.zero) PlayerAcceleration();
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
            _playerVelocity.y = initialJumpVelocity;
            print(_playerVelocity.y);
        }

        if (isHeadHitting && !groundedPlayer)
        {
            _playerVelocity.y += testGravityValue * Time.deltaTime;
        }
    }
    
    private void SetupJump()
    {
        float timeToApex = maxJumpTime / 2;
        //gravityValue = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
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

    public void RbAddForce(float force)
    {
        _rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
    }
    public void AddForce(float force)
    {
        _playerVelocity.y = force;
    }
    

    #endregion
}
