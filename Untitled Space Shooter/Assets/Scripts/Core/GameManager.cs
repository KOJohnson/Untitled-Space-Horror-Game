using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]private bool disableMovement;
    
        [SerializeField]private bool disablePlayerInput;

        // Update is called once per frame
        void Update()
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
    }
}
