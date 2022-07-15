using System;
using UnityEngine;

namespace Core
{
    public class PlayerInputManager : MonoBehaviour
    {

        public static PlayerInputManager Instance;
        public static PlayerInput InputActions;
        
        private Vector2 _mouseInput;
       

        private void Awake()
        {
            InputActions = new PlayerInput();

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            
        }

        public Vector2 PlayerMouseInput()
        {
            _mouseInput = InputActions.Player.MouseVector.ReadValue<Vector2>();
            
            return _mouseInput;
        }
        
        public Vector2 InteractionMouseInput()
        {
            _mouseInput = InputActions.Interaction.MouseVector.ReadValue<Vector2>();
            
            return _mouseInput;
        }

        private void OnEnable()
        {
            InputActions.Enable();
            EventManager.EnableAllInput += EnableInput;
            EventManager.DisableAllInput += DisableInput;
        }

        private void OnDisable()
        {
            InputActions.Disable();
            EventManager.EnableAllInput -= EnableInput;
            EventManager.DisableAllInput -= DisableInput;
        }

        
        private void DisableInput()
        {
            InputActions.Player.Disable();
        }
 
        private void EnableInput()
        {
            InputActions.Player.Enable();
        }
    }
}
