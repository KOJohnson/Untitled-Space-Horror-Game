using System;
using System.Collections;
using UnityEngine;
public class EventManager:MonoBehaviour
{
    public static event Action DisableAllMovement;
    public static event Action EnableAllMovement;

    public static void OnDisableAllMovement()
    {
        DisableAllMovement?.Invoke();
    }
    
    public static void OnEnableAllMovement()
    {
        EnableAllMovement?.Invoke();
    }

}
