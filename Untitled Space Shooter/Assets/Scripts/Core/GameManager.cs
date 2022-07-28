using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
    
        public Transform player;
        public int enemyCount;
        public bool onPause;

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
            

            if (Instance == null)
                Instance = this;
            else
                DontDestroyOnLoad(gameObject);
            
        }

        private void Update()
        {
            if (PlayerInputManager.InputActions.Menu.Pause.WasPerformedThisFrame())
            {
                if (onPause)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        private void PauseGame()
        {
            DisableInput();
            Time.timeScale = 0;
            onPause = true;

            /////EVENTS/////
            //enable pause menu ui
            //disable all player movement 
            //disable all HUD
        }

        private void ResumeGame()
        {
           EnableInput();
            Time.timeScale = 1;
            onPause = false;

            /////EVENTS/////
            //disable pause menu ui
            //enable all player movement 
            //enable all HUD
        }

        public void DisableInput()
        {
            PlayerInputManager.InputActions.Player.Disable();
        }
        
        public void EnableInput()
        {
            PlayerInputManager.InputActions.Player.Enable();
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

        public void AddEnemy()
        {
            enemyCount += 1;
        }
        public void RemoveEnemy()
        {
            enemyCount -= 1;
        }
        
    }
}
