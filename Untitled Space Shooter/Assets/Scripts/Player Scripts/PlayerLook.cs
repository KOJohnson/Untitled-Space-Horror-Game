using Core;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float _xRotation = 0f;
    private float _xClamp = 75f;
    private float _mouseX;
    private float _mouseY;
    
    [SerializeField]private Transform playerBody;
    [SerializeField]private float lookSensitivity = 5f;

    private void Update()
    {
        MouseInputHandler();

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        
        transform.localRotation = Quaternion.Euler(_xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * _mouseX, Space.World);
        
    }
    private void MouseInputHandler()
    {
        _mouseX =  PlayerInputManager.Instance.PlayerMouseInput().x * lookSensitivity * Time.deltaTime;
        _mouseY =  PlayerInputManager.Instance.PlayerMouseInput().y * lookSensitivity * Time.deltaTime;
    }

}
