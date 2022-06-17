using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecontaminationManager : MonoBehaviour
{
    public GameObject door1, door2;
    [SerializeField]private int timer;
    private Animator entryDoor, exitDoor;
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    private void Awake()
    {
        entryDoor = door1.GetComponent<Animator>();
        exitDoor = door2.GetComponent<Animator>();
    }
    public void StartDecontaminationEnter()
    {
        StartCoroutine(Enter());
    }
    
    public void StartDecontaminationExit()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Enter()
    {
        yield return new WaitForSeconds(timer);
        exitDoor.SetBool(OpenDoor, true);
        
    }
    
    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(timer);
        entryDoor.SetBool(OpenDoor, true);
        
    }
}
