using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPaster : MonoBehaviour
{
    [SerializeField]
    private Button pasteButton;

    public string PastedText { get; private set; }

    public string DisplayedText { get; private set; }

    [Header("Add reference to only one of the following:")]

    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private TMP_InputField inputField;

    private void Awake()
    {
        pasteButton.onClick.AddListener(PasteTextFromClipboard);
    }

    private void PasteTextFromClipboard()
    {
        PasteText(TextClipboard.DisplayedText, TextClipboard.CopiedText);
    }

    private void PasteText(string displayedText, string copiedText)
    {
        PastedText = copiedText;

        if (textField != null)
        {
            textField.text = displayedText;
        }
        else if (inputField != null)
        {
            inputField.text = displayedText;
        }

        DisplayedText = displayedText;
    }
}