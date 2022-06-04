using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask interactableLayer;

    private Camera camera;
    private PlayerInput playerInput;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        InputHandler.instance.inputActions.Interaction.Interact.performed += _ => RaycastInteract();
    }
    
    private void RaycastInteract()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayer)) return;
        
        var interactable = hit.collider.GetComponent<IInteractables>();
        interactable?.Interact();
    }
}
