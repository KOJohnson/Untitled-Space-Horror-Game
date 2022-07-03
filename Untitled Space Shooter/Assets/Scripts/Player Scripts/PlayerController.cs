using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;

    public Vector3 movementInput;
    private Vector3 movementDirection;
    private Vector3 slopeMovementDirection;
    private RaycastHit slopeHit;

    [SerializeField] private float gravityValue = -5f;
    public Vector3 playerVelocity = Vector3.zero;

    [SerializeField] private bool useAcceleration;
    [SerializeField] private float airSpeed = 4f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float minSpeed;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    
    
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
        characterController = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        groundedPlayer = IsGrounded();

        MovementInput();
        HandleGravity();

        switch (groundedPlayer && !onSlope)
        {
            case true:
                characterController.Move(movementDirection * (currentSpeed * Time.deltaTime));
                break;
            case false:
                characterController.Move(movementDirection * (airSpeed * Time.deltaTime));
                break;
        }
    }

    #endregion
    

    #region Private Methods
    
    private void DisableMovement()
    {
        InputHandler.instance.inputActions.Player.Move.Disable();
    }
    
    private void EnableMovement()
    {
        InputHandler.instance.inputActions.Player.Move.Enable();
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
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    
    private void MovementInput()
    {
        movementInput = InputHandler.instance.inputActions.Player.Move.ReadValue<Vector2>();
        movementDirection = transform.right * movementInput.x + transform.forward * movementInput.y;

        if (!useAcceleration)
        {
            currentSpeed = maxSpeed;
            return;
        }
        if (movementInput != Vector3.zero) PlayerAcceleration();
        else currentSpeed = 0;
    }

    // private void AdjustHeight(float height)
    // {
    //     float center = height / 2;
    //
    //     characterController.height = Mathf.Lerp(characterController.height, height, crouchSpeed);
    //     characterController.center = Vector3.Lerp(characterController.center, new Vector3(0, center, 0), crouchSpeed);
    //     
    //     collider.height = Mathf.Lerp(collider.height, height, crouchSpeed);
    //     collider.center = Vector3.Lerp(collider.center, new Vector3(0, center, 0), crouchSpeed);
    // }

    private void HandleJump()
    {
        if (InputHandler.instance.inputActions.Player.Jump.WasPressedThisFrame() && groundedPlayer)
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
