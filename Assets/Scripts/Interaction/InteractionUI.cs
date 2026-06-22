using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactionTextField;
    [SerializeField]
    private Image interactionIcon;

    private void Start()
    {
        SetInteractionTextEnabled(false);
        SetInteractionIconEnabled(false);
    }

    public void SetInteractionText(string text)
    {
        interactionTextField.text = text;
    }

    public void SetInteractionTextEnabled(bool enabled)
    {
        interactionTextField.gameObject.SetActive(enabled);
    }

    public void SetInteractionIconEnabled(bool enabled)
    {
        interactionIcon.gameObject.SetActive(enabled);
    }
}
