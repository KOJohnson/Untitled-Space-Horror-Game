using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInspect : MonoBehaviour,IInteractables
{
    public float rotSpeed = 3f;
    public float lerp;
    public float distFromCam;

    public bool isInspecting;
    public bool mouseDown;

    private Vector3 mouseInput;
    private Vector3 rotationVector;
    private float mouseX;
    private float mouseY;
    
    private Vector3 originalPos;
    public Vector3 originalRot;
    private Vector3 targetPos;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;

        InputHandler.instance.inputActions.Interaction.LeftMouse.performed += OnLeftMouseDown;
        InputHandler.instance.inputActions.Interaction.LeftMouse.canceled += OnLeftMouseUp;

    }

    private void OnLeftMouseUp(InputAction.CallbackContext context)
    {
        mouseDown = context.ReadValueAsButton();
    }

    private void OnLeftMouseDown(InputAction.CallbackContext context)
    {
        mouseDown = context.ReadValueAsButton();
    }

    private void Start()
    {
        originalPos = transform.position;
        originalRot = transform.localEulerAngles;
    }
    
    private void Update()
    {
        MouseInput();
        targetPos = camera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,distFromCam));
        
        switch (isInspecting)
        {
            case true:
                EventManager.OnDisableAllInput();
                transform.position = Vector3.Slerp(transform.position, targetPos, lerp);
                if (mouseDown)
                {
                    transform.Rotate(rotationVector * (rotSpeed * Time.deltaTime), Space.World);
                }
                break;
            case false:
                EventManager.OnEnableAllInput();
                transform.ResetRotation(originalRot);
                transform.position = Vector3.Slerp(transform.position, originalPos, lerp);
                break;
        }
        
    }

    public void Interact()
    {
        switch (isInspecting)
        {
            case true:
                isInspecting = false;
                break;
            case false:
                isInspecting = true;
                break;
        }
    }
    
    private void MouseInput()
    {
        mouseInput = InputHandler.instance.inputActions.Interaction.MouseVector.ReadValue<Vector2>();
        rotationVector = new Vector3(mouseInput.y, mouseInput.x, 0);
    }
}
