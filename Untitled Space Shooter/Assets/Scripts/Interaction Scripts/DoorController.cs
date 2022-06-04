using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractables
{
    [SerializeField] private bool isDoorOpen;
    private Animator animator;

    public GameObject door;

    private void Awake()
    {
        animator = door.GetComponent<Animator>();
    }

    private void Start()
    {
        
        
    }

    private void Update()
    {
        
        
    }

    public void Interact()
    {
        switch (isDoorOpen)
        {
            case false:
                //open the door....
                isDoorOpen = true;
                animator.Play("OpenDoor");
                break;
            case true:
                isDoorOpen = false;
                Debug.Log("closing door");
                break;
        }
    }
}
