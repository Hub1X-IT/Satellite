using UnityEngine;

public class InteractionVisual : MonoBehaviour {


    [SerializeField] private Outline outline;

    [SerializeField] private string interactMessage;
    

    void Start() {
        // outline = GetComponent<Outline>();
        EnableOutline(false);
    }

    public void EnableOutline(bool targetState) {
        if(outline != null) outline.enabled = targetState;
    }

    public string GetInteractMessage() {
        return interactMessage;
    }
}
