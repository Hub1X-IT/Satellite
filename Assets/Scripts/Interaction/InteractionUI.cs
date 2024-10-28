using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactionTextField;


    private string interactionText;


    private bool isInteractionTextEnabled;


    public string InteractionText
    {
        get => interactionText;
        set
        {
            // Set interaction text.
            interactionText = value;
            interactionTextField.text = value;
        }
    }


    public bool IsInteractionTextEnabled
    {
        get => isInteractionTextEnabled;
        set
        {
            // Enable/disable interaction text field
            isInteractionTextEnabled = value;
            interactionTextField.gameObject.SetActive(value);
        }
    }


    private void Awake()
    {
        IsInteractionTextEnabled = false;
    }
}
