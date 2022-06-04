using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{

    private Camera playerCamera;
    public int turnSpeed;
    public float dot;

    private Vector3 playerTemp;
    private Vector3 enemyTemp;
    
    public bool behind;
    public bool infront;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyTemp = new Vector3(0, 0, transform.position.z);
        playerTemp = new Vector3(0, 0, playerCamera.transform.position.z);
        
        var lookRotation = transform.position - playerCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(lookRotation * turnSpeed);

        var dirToTarget = Vector3.Normalize(enemyTemp - playerTemp);

        dot = Vector3.Dot(playerCamera.transform.forward, dirToTarget);

        behind = dot < -0.707;
        infront = dot > 0.707;
        

    }
}
