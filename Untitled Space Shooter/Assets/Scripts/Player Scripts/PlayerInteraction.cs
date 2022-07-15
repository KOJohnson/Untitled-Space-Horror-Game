using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask interactableLayer;

    private Camera _camera;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _camera = Camera.main;
        PlayerInputManager.InputActions.Interaction.Interact.performed += _ => RaycastInteract();
    }
    private void RaycastInteract()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayer)) return;
        
        var interactable = hit.collider.GetComponent<IInteractables>();
        interactable?.Interact();
    }
}
