using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator doorAnimator;

    private const string DOOR_OPEN = "DoorOpen";
    private const string DOOR_CLOSE = "DoorClose";

    private bool doorOpened = false;
    public Transform GetTransform() { return transform; }

    public void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (doorOpened)
        {
            doorOpened = true;
            doorAnimator.SetTrigger(DOOR_OPEN);
        }
        else
        {
            doorOpened = false;
            doorAnimator.SetTrigger(DOOR_CLOSE);
        }
    }
}
