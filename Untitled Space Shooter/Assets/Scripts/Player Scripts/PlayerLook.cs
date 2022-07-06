using Core;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Vector3 _mouseInput;
    private float xRotation = 0f;
    private float xClamp = 75f;
    private float mouseX;
    private float mouseY;
    
    [SerializeField]private Transform playerBody;
    [SerializeField]private float camSensitivity;

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

}
