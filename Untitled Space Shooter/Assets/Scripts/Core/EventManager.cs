using System;
using UnityEngine;

namespace Core
{
    public class EventManager:MonoBehaviour
    {
        public static event Action DisableAllMovement;
        public static event Action EnableAllMovement;
        public static event Action DisableAllInput;
        public static event Action EnableAllInput;

        public static void OnDisableAllMovement()
        {
            DisableAllMovement?.Invoke();
        }
    
        public static void OnEnableAllMovement()
        {
            EnableAllMovement?.Invoke();
        }
    
        public static void OnEnableAllInput()
        {
            EnableAllInput?.Invoke();
        }
    
        public static void OnDisableAllInput()
        {
            DisableAllInput?.Invoke();
        }

    }
}
