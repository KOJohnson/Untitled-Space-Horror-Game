using Cinemachine;
using Core;
using Core.ThirdPersonController;
using UnityEngine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera normalVirtualCamera;
    [SerializeField]private CinemachineVirtualCamera aimVirtualCamera;
    private ThirdPersonController thirdPersonController;

    [SerializeField]private int priorityBoostAmount = 10;
    
    private void Awake()
    {
        normalVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        
        
        InputHandler.instance.inputActions.Player.Aim.performed += _ => StartAim();
        InputHandler.instance.inputActions.Player.Aim.canceled += _ => CancelAim();
    }

    private void StartAim()
    {
        normalVirtualCamera.Priority -= priorityBoostAmount;
    }

    private void CancelAim()
    {
        normalVirtualCamera.Priority += priorityBoostAmount;
    }
}
