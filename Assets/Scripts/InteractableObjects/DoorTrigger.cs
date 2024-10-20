using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable {


    public event EventHandler OnDoorInteract;


    private void Start() {
        // gameObject.layer = InteractionData.Instance.DefaultInteractableLayerMask;
    }


    public void Interact() {
        OnDoorInteract?.Invoke(this, EventArgs.Empty);
    }


    public Transform GetTransform() { return transform; }
}
