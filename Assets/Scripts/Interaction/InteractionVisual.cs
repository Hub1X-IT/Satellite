using UnityEngine;

public class InteractionVisual : MonoBehaviour
{
    [SerializeField] private Outline outline;


    [SerializeField] private string interactMessage;
    public string InteractMessage => interactMessage;


    private bool isEnabled;
    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            if (outline != null)
            {
                outline.enabled = value;
            }
            isEnabled = value;
        }
    }


    void Start()
    {
        // outline = GetComponent<Outline>();
        IsEnabled = false;
    }
}
