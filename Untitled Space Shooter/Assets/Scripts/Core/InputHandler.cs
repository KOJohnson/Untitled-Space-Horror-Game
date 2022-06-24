using UnityEngine;

namespace Core
{
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
   EventManager.EnableAllInput += EnableInput;
   EventManager.DisableAllInput += DisableInput;

  }

  private void OnDisable()
  {
   inputActions.Disable();
   EventManager.EnableAllInput -= EnableInput;
   EventManager.DisableAllInput -= DisableInput;
  }

  private void DisableInput()
  {
   inputActions.Player.Disable();
  }
 
  private void EnableInput()
  {
   inputActions.Player.Enable();
  }
 
 }
}
