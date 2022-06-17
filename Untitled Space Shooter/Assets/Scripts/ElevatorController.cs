using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ElevatorController : MonoBehaviour, IInteractables
{
   
    
    public float t;
    
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private GameObject elevator;

    // Update is called once per frame
    void Update()
    {
        var distanceToTarget = Vector3.Distance(elevator.transform.position, targetLocation.position);
        
        if (isMoving)
        {
            //elevator.transform.DOMove(elevator.transform.position, targetLocation.position.y, false);
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, targetLocation.position, t);
        }
        
        if (distanceToTarget <= 0.1)
        {
            isMoving = false;
        }
    }

    public void Interact()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }

   
}
