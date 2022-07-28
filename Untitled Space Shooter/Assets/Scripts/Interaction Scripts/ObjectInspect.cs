using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = Core.PlayerInputManager;

public class ObjectInspect : MonoBehaviour,IInteractables
{
    public float rotSpeed = 3f;
    public float lerp;
    public float distFromCam;

    public bool canCollect;
    public bool isInspecting;
    public bool mouseDown;

    private Vector2 _mouseInput;
    private Vector2 _rotationVector;
    private float _mouseX;
    private float _mouseY;
    
    private Vector3 _originalPos;
    private Vector3 _originalRot;
    private Vector3 _targetPos;
    public Camera _objectCamera;

    private void Awake()
    {
        PlayerInputManager.InputActions.Interaction.LeftMouse.performed += OnLeftMouseDown;
        PlayerInputManager.InputActions.Interaction.LeftMouse.canceled += OnLeftMouseUp;
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
        _originalPos = transform.position;
        _originalRot = transform.localEulerAngles;
    }
    
    private void Update()
    {
        MouseInput();
        _targetPos = _objectCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,distFromCam));

        if (isInspecting)
        {
            //GameManager.Instance.DisableInput();
            transform.position = Vector3.Slerp(_originalPos, _targetPos, lerp);
            
            if (mouseDown)
                transform.Rotate(_rotationVector * (rotSpeed * Time.deltaTime), Space.World);
            
        }
        else
        {
            //GameManager.Instance.EnableInput();
            transform.ResetRotation(_originalRot);
            transform.position = Vector3.Slerp(transform.position, _originalPos, lerp);
        }
        
        
        // switch (isInspecting)
        // {
        //     case true:
        //         GameManager.Instance.DisableInput();
        //         transform.position = Vector3.Slerp(transform.position, _targetPos, lerp);
        //         if (mouseDown)
        //         {
        //             transform.Rotate(_rotationVector * (rotSpeed * Time.deltaTime), Space.World);
        //         }
        //         break;
        //     case false:
        //         GameManager.Instance.EnableInput();
        //         transform.ResetRotation(_originalRot);
        //         transform.position = Vector3.Slerp(transform.position, _originalPos, lerp);
        //         break;
        // }
        
    }

    public void Interact()
    {
        switch (isInspecting)
        {
            case true:
                isInspecting = false;
                _objectCamera.depth = -100;
                break;
            case false:
                isInspecting = true;
                _objectCamera.depth = 100;
                break;
        }
    }
    
    private void MouseInput()
    {
        _mouseInput = PlayerInputManager.Instance.InteractionMouseInput();
        _rotationVector = new Vector2(_mouseInput.y, _mouseInput.x);
    }
}
