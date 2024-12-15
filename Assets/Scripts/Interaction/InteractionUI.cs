using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactionTextField;

    private void Awake()
    {
        SetInteractionTextEnabled(false);
    }

    public void SetInteractionText(string text)
    {
        interactionTextField.text = text;
    }

    public void SetInteractionTextEnabled(bool enabled)
    {
        interactionTextField.gameObject.SetActive(enabled);
    }
}
