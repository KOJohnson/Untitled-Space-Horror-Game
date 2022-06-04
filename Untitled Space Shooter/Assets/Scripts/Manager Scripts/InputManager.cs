using System;
using UnityEngine;

    public class InputManager: MonoBehaviour
    {
        public static InputManager instance;
        public PlayerInput PlayerInput;
        private void Awake()
        {
            PlayerInput = new PlayerInput();
            
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            PlayerInput.Enable();
        }

        private void OnDisable()
        {
            PlayerInput.Disable();
        }
    }
