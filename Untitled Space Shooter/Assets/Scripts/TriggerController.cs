using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{
    private Animator anim;
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");

    [SerializeField] private UnityEvent onTriggerHit;
    private void Awake()
    {
        gameObject.SetActive(false);
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool(OpenDoor, false);
            onTriggerHit?.Invoke();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
