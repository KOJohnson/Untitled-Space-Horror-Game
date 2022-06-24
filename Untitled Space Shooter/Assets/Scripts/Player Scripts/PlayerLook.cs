using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Vector3 _mouseInput;
    private float xRotation = 0f;
    private float xClamp = 75f;
    private float mouseX;
    private float mouseY;
    
    public Transform playerBody;
    [SerializeField] private float camSensitivity;

    private void OnEnable()
    {
        EventManager.DisableAllMovement += DisableMouseInput;
        EventManager.EnableAllMovement += EnableMouseInput;
    }

    private void OnDisable()
    {
        EventManager.DisableAllMovement -= DisableMouseInput;
        EventManager.EnableAllMovement -= EnableMouseInput;
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
        InputHandler.instance.inputActions.Player.MouseVector.Disable();
    }
    
    private void EnableMouseInput()
    {
        InputHandler.instance.inputActions.Player.MouseVector.Enable();
    }
}
