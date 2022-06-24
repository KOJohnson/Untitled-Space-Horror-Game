using Core;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth;
    public float intensity;

    private Vector3 mouseInput;
    private Quaternion originRotation;
    private PlayerInput playerInput;
    
    private float xRotation = 0f;
    private float xClamp = 75f;

    #region MonoBehaviour CallBacks

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    void Update()
    {
        UpdateSway();
    }
    
    #endregion

    #region Private Mehtods

    private void UpdateSway()
    {
        mouseInput = InputHandler.instance.inputActions.Player.MouseVector.ReadValue<Vector2>();
        float mouseX = mouseInput.x;
        float mouseY = mouseInput.y;

        xRotation = -mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        //calculate target rotation
        Quaternion xAxisAdjsutment = Quaternion.AngleAxis(-intensity * mouseX , Vector3.up);
        Quaternion yAxisAdjsutment = Quaternion.AngleAxis(intensity * mouseY , Vector3.right);
        Quaternion targetRotation = originRotation * xAxisAdjsutment * yAxisAdjsutment;

        //rotate towards target rotation 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

    }

    #endregion

    
}
