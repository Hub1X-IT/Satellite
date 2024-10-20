using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable {


    public void Interact() {
        gameObject.SetActive(false);
    }

    public Transform GetTransform() { return transform; }
}
