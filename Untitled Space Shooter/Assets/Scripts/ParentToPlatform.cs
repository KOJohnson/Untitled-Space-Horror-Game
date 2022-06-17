using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }
}
