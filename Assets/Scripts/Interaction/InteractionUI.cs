using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactionTextField;

    public string InteractionText { get; private set; }

    public bool IsInteractionTextActive { get; private set; }


    private void Awake()
    {
        SetInteractionTextEnabled(false);
    }

    public void SetInteractionText(string text)
    {
        InteractionText = text;
        interactionTextField.text = text;
    }

    public void SetInteractionTextEnabled(bool enabled)
    {
        IsInteractionTextActive = enabled;
        interactionTextField.gameObject.SetActive(enabled);
    }
}
