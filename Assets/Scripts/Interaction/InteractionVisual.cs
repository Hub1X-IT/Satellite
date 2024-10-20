using UnityEngine;

public class InteractionVisual : MonoBehaviour
{
    private Outline outline;

    [SerializeField] private string interactMessage;


    void Start() {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void EnableOutline() {
        outline.enabled = true;
    }

    public void DisableOutline() {
        outline.enabled = false;
    }

    public string GetInteractMessage() {
        return interactMessage;
    }
}
