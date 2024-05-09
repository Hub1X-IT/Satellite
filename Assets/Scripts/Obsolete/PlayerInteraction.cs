/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    InteractVisual currentInteractable;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if(Input.GetKey(KeyCode.F) && currentInteractable != null) 
        {
            // currentInteractable.Interact();
        }
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit, playerReach)) 
        {
            if (hit.collider.tag == "Interactable")
            {
                InteractVisual newInteractable = hit.collider.GetComponent<InteractVisual>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
    }

    private void SetNewCurrentInteractable(InteractVisual newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        InteractionHudManager.Instance.EnableInteractionText(currentInteractable.GetInteractMessage());
    }


    private void DisableCurrentInteractable()
    {
        InteractionHudManager.Instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
*/