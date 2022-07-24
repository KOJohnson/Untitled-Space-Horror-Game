using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UnityEvent myEvent;

        public Transform player;

        public int enemyCount;

        public bool waveOneComplete;
        public bool waveTwoComplete;
        public bool waveThreeComplete;

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
            

            if (Instance == null)
                Instance = this;
            else
                DontDestroyOnLoad(gameObject);
            
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
