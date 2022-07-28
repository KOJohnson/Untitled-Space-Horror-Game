using Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteraction : MonoBehaviour,IInteractables
{
    public UnityEvent doorEvents;
    
    public void Interact()
    {
        doorEvents?.Invoke();
    }
}
