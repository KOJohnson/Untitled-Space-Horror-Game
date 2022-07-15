using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractables
{
    [SerializeField] private AnimationClip clip;
    [SerializeField] private bool isDoorOpen;
    private Animator animator;

    public GameObject door;

    private void Awake()
    {
        animator = door.GetComponent<Animator>();
    }

    public void Interact()
    {
        switch (isDoorOpen)
        {
            case false:
                //open the door....
                isDoorOpen = true;
                AnimationManager.Instance.PlayAnimation(clip);
                break;
            case true:
                isDoorOpen = false;
                Debug.Log("closing door");
                break;
        }
    }
}
