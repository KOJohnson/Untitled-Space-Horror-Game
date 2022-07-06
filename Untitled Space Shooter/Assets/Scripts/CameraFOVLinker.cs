using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFOVLinker : MonoBehaviour
{
    public Camera mainCamera;
    public Camera fpsCam;
    
    private void Update()
    {
        if (fpsCam == null)
        {
            Debug.LogError("Missing FPS Camera");
            return;
        }
        
        fpsCam.fieldOfView = mainCamera.fieldOfView;
    }
}
