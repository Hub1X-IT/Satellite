using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractVisual : MonoBehaviour
{
    private Outline outline;

    [SerializeField] private string interactMessage;
    

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    
    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public string GetInteractMessage() {
        return interactMessage;
    }
}
