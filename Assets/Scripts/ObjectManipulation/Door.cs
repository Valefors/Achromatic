using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void SetInteractionMode()
    {
        base.SetInteractionMode();
        OpenDoor();
    }

    void OpenDoor()
    {
        if (animator != null) animator.SetBool("IsOpen", !animator.GetBool("IsOpen"));
    }
}
