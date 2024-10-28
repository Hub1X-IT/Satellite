using UnityEngine;

public class InteractionVisual : MonoBehaviour
{
    [SerializeField]
    private Outline outline;

    [SerializeField]
    private string interactMessage;

    private bool isEnabled;

    public string InteractMessage => interactMessage;

    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            // Enable/disable interaction visual.
            isEnabled = value;
            EnableDisableInteractionVisual(value);
        }
    }


    private void Start()
    {
        IsEnabled = false;
    }


    private void EnableDisableInteractionVisual(bool enabled)
    {
        if (outline != null)
        {
            outline.enabled = enabled;
        }
    }
}
