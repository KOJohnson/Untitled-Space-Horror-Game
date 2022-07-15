using System;
using UnityEngine;

namespace Core
{
    public class WeaponTilt : MonoBehaviour
    {

        private Vector3 _currentRotation;
        private Vector3 _targetRotation;
        private Vector3 _originRotation;

        public float tiltSnappiness;
        public float tiltReturnSpeed;
        
        public float tiltAmount;

        private Vector3 _movementDirection;

        private void Start()
        {
            _originRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        private void Update()
        {
            _movementDirection = PlayerInputManager.InputActions.Player.Move.ReadValue<Vector2>();
            Tilt(tiltReturnSpeed, tiltSnappiness);

            if (_movementDirection == Vector3.right)
            {
                TiltFire(-tiltAmount);
            }

            if (_movementDirection == -Vector3.right)
            {
                TiltFire(tiltAmount);
            }

            
        }

        private void Tilt(float returnSpeed, float snappiness)
        {
            _targetRotation = Vector3.Lerp(_targetRotation, _originRotation, returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }
    
        private void TiltFire(float tiltZ)
        {
            _targetRotation += new Vector3(0,0,tiltZ);
        }
    }
}
