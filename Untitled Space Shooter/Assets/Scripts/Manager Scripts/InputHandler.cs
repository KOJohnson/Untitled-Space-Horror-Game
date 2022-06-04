using UnityEngine;

public class InputHandler : MonoBehaviour
{
 public static InputHandler instance;
 public PlayerInput inputActions;
 private void Awake()
 {
  inputActions = new PlayerInput();
  
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
  inputActions.Enable();
 }

 private void OnDisable()
 {
  inputActions.Disable();
 }
}
