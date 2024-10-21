using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable {


    public void Interact() {
        gameObject.SetActive(false);
    }

    public InteractionVisual GetInteractionVisual() { return null; }

    public Transform GetTransform() { return transform; }
}
