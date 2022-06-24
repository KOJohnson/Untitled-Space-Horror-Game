using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Framerate")]
        public int frameRate;
        
        [SerializeField]private bool disableMovement;
    
        [SerializeField]private bool disablePlayerInput;

        private void OnEnable()
        {
            EventManager.EnableCursor += MouseCursorOn;
            EventManager.DisableCursor += MouseCursorOff;
        }

        private void OnDisable()
        {
            EventManager.EnableCursor -= MouseCursorOn;
            EventManager.DisableCursor -= MouseCursorOff;
        }

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            Application.targetFrameRate = frameRate;
        }

        private void Update()
        {

            if (disableMovement)
            {
                EventManager.OnDisableAllMovement();
            }
            else
            {
                EventManager.OnEnableAllMovement();
            }
        
            if (disablePlayerInput)
            {
                EventManager.OnDisableAllInput();
            }
            else
            {
                EventManager.OnEnableAllInput();
            }

        }

        private void MouseCursorOn()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void MouseCursorOff()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }
}
