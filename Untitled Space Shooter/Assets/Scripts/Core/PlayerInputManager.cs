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

        public Vector2 PlayerMouseInput()
        {
            _mouseInput = InputActions.Player.MouseVector.ReadValue<Vector2>();
            
            return _mouseInput;
        }

        public Vector2 PlayerMovementInput()
        {
            var movementDirection = InputActions.Player.Move.ReadValue<Vector2>();

            return movementDirection;
        }
        
        public Vector2 InteractionMouseInput()
        {
            _mouseInput = InputActions.Interaction.MouseVector.ReadValue<Vector2>();
            
            return _mouseInput;
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}
