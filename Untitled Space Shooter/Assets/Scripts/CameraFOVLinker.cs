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
        fpsCam.fieldOfView = mainCamera.fieldOfView;
    }
}
