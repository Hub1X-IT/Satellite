using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {


    float interactRange = 2f;


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        IInteractable interactable = GetInteractableObject();
        interactable?.Interact(); // ? -> Check if not null
    }

    public IInteractable GetInteractableObject() {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out IInteractable interactable)) {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList) {
            if (closestInteractable == null) {
                closestInteractable = interactable;
            }
            else {
                // Calculate and compare distance
                if ((transform.position - interactable.GetTransform().position).sqrMagnitude < 
                    (transform.position - closestInteractable.GetTransform().position).sqrMagnitude) {
                    // Closer
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}