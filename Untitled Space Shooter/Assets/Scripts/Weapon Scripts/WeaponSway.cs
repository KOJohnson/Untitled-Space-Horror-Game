using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth;
    public float intensity;

    private Vector3 mouseInput;
    private Quaternion originRotation;
    private PlayerInput playerInput;

    #region MonoBehaviour CallBacks
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    
    void Update()
    {
        UpdateSway();
    }
    
    #endregion

    #region Private Mehtods

    private void UpdateSway()
    {
        mouseInput = playerInput.Player.MouseVector.ReadValue<Vector2>();
        float mouseX = mouseInput.x;
        float mouseY = mouseInput.y;

        //calculate target rotation
        Quaternion xAxisAdjsutment = Quaternion.AngleAxis(-intensity * mouseX , Vector3.up);
        Quaternion yAxisAdjsutment = Quaternion.AngleAxis(intensity * mouseY , Vector3.right);
        Quaternion targetRotation = originRotation * xAxisAdjsutment * yAxisAdjsutment;
    
        //rotate towards target rotation 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);

    }

    #endregion

    
}
