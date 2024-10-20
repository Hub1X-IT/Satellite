using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable {

    public event EventHandler OnDeskTrigger;


    public void Interact() {
        OnDeskTrigger?.Invoke(this, EventArgs.Empty);
        // Debug.Log("DeskTrigger: Interact()"); // !!!!!!!!!! Debug Log !!!!!!!!!!
    }


    public Transform GetTransform() { return transform; }
}