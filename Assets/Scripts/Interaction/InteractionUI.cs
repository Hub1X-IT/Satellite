using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactionTextField;

    private string interactionText;
    public string InteractionText
    {
        get => interactionText;
        set
        {
            interactionTextField.text = value;
            interactionText = value;
        }
    }

    private bool isInteractionTextEnabled;
    public bool IsInteractionTextEnabled
    {
        get => isInteractionTextEnabled;
        set
        {
            interactionTextField.gameObject.SetActive(value);
            isInteractionTextEnabled = value;
        }
    }


    private void Awake()
    {
        IsInteractionTextEnabled = false;
    }
}
