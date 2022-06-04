using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector3 _mouseInput;
    private float xRotation = 0f;
    private float xClamp = 75f;
    private float mouseX;
    private float mouseY;
    
    public Transform playerBody;
    [SerializeField] private float camSensitivity;

    private void OnEnable()
    {
        playerInput.Enable();
        EventManager.DisableAllMovement += DisableMouseInput;
        EventManager.EnableAllMovement += EnableMouseInput;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        EventManager.DisableAllMovement -= DisableMouseInput;
        EventManager.EnableAllMovement -= EnableMouseInput;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        MouseInputHandler();

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX, Space.World);
        
    }
    private void MouseInputHandler()
    {
        _mouseInput = InputHandler.instance.inputActions.Player.MouseVector.ReadValue<Vector2>();
        mouseX = _mouseInput.x * camSensitivity * Time.deltaTime;
        mouseY = _mouseInput.y * camSensitivity * Time.deltaTime;
    }

    private void DisableMouseInput()
    {
        playerInput.Player.MouseVector.Disable();
    }
    
    private void EnableMouseInput()
    {
        playerInput.Player.MouseVector.Enable();
    }
}
