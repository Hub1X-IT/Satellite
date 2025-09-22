using UnityEngine;

public class InteractionVisual : MonoBehaviour
{
    [SerializeField]
    private Outline outline;

    [SerializeField]
    private string interactMessage;

    public bool IsEnabled { get; private set; }

    public string InteractMessage => interactMessage;


    private void Start()
    {
        SetInteractionVisualEnabled(false);
    }

    public void SetInteractionVisualEnabled(bool enabled)
    {
        IsEnabled = enabled;
        if (outline != null)
        {
            outline.enabled = enabled;
        }
    }

    public void SetInteractMessage(string newMessage)
    {
        interactMessage = newMessage;
    }
}
