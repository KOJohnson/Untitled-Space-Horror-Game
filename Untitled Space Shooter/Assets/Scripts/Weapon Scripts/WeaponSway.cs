using Core;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth;
    public float intensity;

    private Vector2 _mouseInput;
    private Quaternion _originRotation;
    private PlayerInput _playerInput;
    
    private float _xRotation = 0f;
    private float _xClamp = 75f;

    #region MonoBehaviour CallBacks

    private void Start()
    {
        _originRotation = transform.localRotation;
    }

    void Update()
    {
        UpdateSway();
    }
    
    #endregion

    #region Private Mehtods

    private void UpdateSway()
    {
        _mouseInput = PlayerInputManager.Instance.PlayerMouseInput();
        float mouseX = _mouseInput.x;
        float mouseY = _mouseInput.y;

        _xRotation = -mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);

        //calculate target rotation
        Quaternion xAxisAdjsutment = Quaternion.AngleAxis(-intensity * mouseX , Vector3.up);
        Quaternion yAxisAdjsutment = Quaternion.AngleAxis(intensity * mouseY , Vector3.right);
        Quaternion targetRotation = _originRotation * xAxisAdjsutment * yAxisAdjsutment;

        //rotate towards target rotation 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

    }

    #endregion

    
}
