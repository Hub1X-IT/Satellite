using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;
    

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    
    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
