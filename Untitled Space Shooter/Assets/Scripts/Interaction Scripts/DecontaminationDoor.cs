using Core.Interfaces;
using UnityEngine;

public class DecontaminationDoor : MonoBehaviour, IInteractables
{
    private Animator _anim;
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");

    [SerializeField] private GameObject entryTrigger;

    private bool _isDoorOpen;

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (!_isDoorOpen)
        {
            entryTrigger.SetActive(true);
            _anim.SetBool(OpenDoor, true);
        }
    }
    
}
