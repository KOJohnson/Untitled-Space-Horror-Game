
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private Animator anim;
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool(OpenDoor, false);
        }
    }
}
