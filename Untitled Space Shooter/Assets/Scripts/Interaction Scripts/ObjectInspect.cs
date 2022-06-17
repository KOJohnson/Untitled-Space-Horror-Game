using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class ObjectInspect : MonoBehaviour,IInteractables
{
    public float lerp;
    public float distFromCam;

    public bool isInspecting;
    
    private Vector3 originalPos;
    private Vector3 targetPos;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = camera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,distFromCam));
        switch (isInspecting)
        {
            case true:
                EventManager.OnDisableAllMovement();
                transform.position = Vector3.Slerp(transform.position, targetPos, lerp);
                break;
            case false:
                EventManager.OnEnableAllMovement();
                transform.position = Vector3.Slerp(transform.position, originalPos, lerp);
                break;
        }
        
    }

    public void Interact()
    {
        switch (isInspecting)
        {
            case true:
                isInspecting = false;
                break;
            case false:
                isInspecting = true;
                break;
        }
        
    }
}
