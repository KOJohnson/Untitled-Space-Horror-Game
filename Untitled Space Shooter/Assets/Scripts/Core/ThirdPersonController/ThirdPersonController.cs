using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Cinemachine;

namespace Core.ThirdPersonController
{
    public class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        private CharacterController characterController;
        private Vector2 movementInput;
        private Vector3 movementDirection;
        
        [SerializeField] private float gravityValue = -5f;
        private Vector3 playerVelocity = Vector3.zero;

        [SerializeField] private float acceleration = 0.1f;
        [SerializeField]private float velocity;
        [SerializeField]private float walkSpeed = 1.5f;
        [SerializeField]private float runSpeed = 3f;
        [SerializeField]private float currentSpeed;
        [SerializeField]private float rotationSpeed = 300;
        [SerializeField]private float aimRotationSpeed = 600;
        
        [SerializeField]private bool isPlayerGrounded;
        public bool isAiming {get; private set;}
        [SerializeField]private bool isSprinting;

        [SerializeField]private Transform checkSphere;
        [SerializeField]private float sphereRadius;
        [SerializeField]private LayerMask whatIsGround;
    
        //change later
        public Camera cam;
        public GameObject crosshairCanvas;
        //
        
        private int velocityZHash;
        private int velocityXHash;
        private int animIsAiming;

        private void OnEnable()
        {
            EventManager.EnableCrosshair += CrosshairUIOn;
            EventManager.DisableCrosshair += CrosshairUIOff;
        }

        private void OnDisable()
        {
            EventManager.EnableCrosshair -= CrosshairUIOn;
            EventManager.DisableCrosshair -= CrosshairUIOff;
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();

            PlayerInputManager.InputActions.Player.Aim.performed += _ => isAiming = true;
            PlayerInputManager.InputActions.Player.Aim.canceled += _ => isAiming = false;
            
            PlayerInputManager.InputActions.Player.Sprint.performed += _ => isSprinting = true;
            PlayerInputManager.InputActions.Player.Sprint.canceled += _ => isSprinting = false;

            velocityZHash = Animator.StringToHash("velocityZ");
            velocityXHash = Animator.StringToHash("velocityX");
            animIsAiming = Animator.StringToHash("isAiming");
        }

        private void Update()
        {
            isPlayerGrounded = IsGrounded();
            movementInput = PlayerInputManager.Instance.PlayerMovementInput();
            
            HandleGravity();
            PlayerMovement();
            AnimationController();
        }
        
        private void PlayerMovement()
        {
            //Get Camera Direction Vectors
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            forward.y = 0;
            right.y = 0;
            forward = forward.normalized;
            right = right.normalized;

            //Set Player Input Relative to camera 
            movementDirection = right * movementInput.x + forward * movementInput.y;

            characterController.Move(movementDirection * (currentSpeed * Time.deltaTime));
            
            //Rotate towards camera direction
            if (movementDirection != Vector3.zero && !isAiming)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }
            
            if (isAiming)
            {
                //turn the player to face camera forward direction if not already
                Quaternion desiredRotation = Quaternion.LookRotation(forward, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, aimRotationSpeed * Time.deltaTime);
                
                EventManager.OnEnableCrosshair();
            }
            else
                EventManager.OnDisableCrosshair();
        }
        
        private void AnimationController()
        {
            //Need to get strafe animations

            //if theres no movement input play idle animation
            
            //if there is movement input but no left shift play walk animation
            
            //if left shift pressed while moving play sprint animation

            currentSpeed = isSprinting ? runSpeed : walkSpeed;
            
            anim.SetFloat(velocityZHash, movementInput.y);
            anim.SetFloat(velocityXHash, movementInput.x);
            anim.SetBool(animIsAiming, isAiming);
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(checkSphere.position, sphereRadius, whatIsGround);
        }
        
        private void HandleGravity()
        {
            if (isPlayerGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
        
            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);
        }

        private void CrosshairUIOn()
        {
            crosshairCanvas.SetActive(true);
        }
        
        private void CrosshairUIOff()
        {
            crosshairCanvas.SetActive(false);
        }

    }
}

