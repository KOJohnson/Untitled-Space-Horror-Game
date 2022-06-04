using UnityEngine;

public class DecontaminationDoor : MonoBehaviour, IInteractables
{
    private Animator anim;
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");

    private bool isDoorOpen;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (!isDoorOpen)
        {
            anim.SetBool(OpenDoor, true);
        }
    }
    
}
