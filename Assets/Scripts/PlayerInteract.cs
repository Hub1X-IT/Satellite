using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {


    [SerializeField] private Transform cameraFollowObject;


    [SerializeField] private LayerMask interactableLayerMasks;


    private readonly float raycastInteractRange = 1f;


    // private float interactRange = 2f;


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        Debug.Log("Interact");
        IInteractable interactable = GetInteractableObject();
        interactable?.Interact(); // ? -> Check if not null
    }

    /*
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
    */


    public IInteractable GetInteractableObject() {        
        /*
        Returns the IInteractable component of an object hit by a raycast.
        If the object doesn't have the component, returns the IInteractable component of the parent object.
        If neither the object nor any parent object have the IInteractable component, or the raycast doesn't hit anything, returns null.
        */
        
        if (Physics.Raycast(cameraFollowObject.position, cameraFollowObject.forward, out RaycastHit hit, raycastInteractRange)) {

            Debug.Log(hit.transform.gameObject);

            if (hit.transform.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                return interactable;
            }
            interactable = hit.transform.gameObject.GetComponentInParent<IInteractable>();
            return interactable;
        }
        return null;
    }
}