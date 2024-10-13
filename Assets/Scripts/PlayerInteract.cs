using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        Debug.Log("Interact"); // !!!!!!!!!!!!!!!!!!!!!!!! Debug log !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        IInteractable interactable = GetInteractableObject();
        interactable?.Interact();
    }


    public IInteractable GetInteractableObject() {
        
        if (Physics.Raycast(PlayerInteractData.Instance.cameraFollowObject.position, PlayerInteractData.Instance.cameraFollowObject.forward, out RaycastHit hit, 
            PlayerInteractData.Instance.interactRange, PlayerInteractData.Instance.interactableLayerMasks)) {

            // !!!!!!!!!!!!!!!!!!!!!!!! Debug log !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Debug.Log(hit.transform.gameObject);

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            return interactable;
            
        }

        return null;
    }
}