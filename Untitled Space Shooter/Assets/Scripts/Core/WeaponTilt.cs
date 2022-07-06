using System;
using UnityEngine;

namespace Core
{
    public class WeaponTilt : MonoBehaviour
    {

        private Vector3 currentRotation;
        private Vector3 targetRotation;
        private Vector3 originRotation;

        public float tiltSnappiness;
        public float tiltReturnSpeed;
        
        public float tiltAmount;

        private Vector3 movementDirection;

        private void Start()
        {
            originRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        private void Update()
        {
            movementDirection = InputHandler.instance.inputActions.Player.Move.ReadValue<Vector2>();
            Tilt(tiltReturnSpeed, tiltSnappiness);

            if (movementDirection == Vector3.right)
            {
                TiltFire(-tiltAmount);
            }

            if (movementDirection == -Vector3.right)
            {
                TiltFire(tiltAmount);
            }

            
        }

        private void Tilt(float returnSpeed, float snappiness)
        {
            targetRotation = Vector3.Lerp(targetRotation, originRotation, returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    
        private void TiltFire(float tiltZ)
        {
            targetRotation += new Vector3(0,0,tiltZ);
        }
    }
}
