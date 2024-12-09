using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextCopier : MonoBehaviour
{
    [SerializeField]
    private Button copyButton;

    public string DisplayedText { get; set; }

    public string TextToCopy
    {
        get
        {
            if (textField != null)
            {
                return textField.text;
            }
            else if (inputField != null)
            {
                return inputField.text;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    [Header("Add reference to only one of the following:")]

    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private TMP_InputField inputField;

    private void Awake()
    {
        copyButton.onClick.AddListener(CopyTextToClipboard);
    }

    private void CopyTextToClipboard()
    {
        TextClipboard.DisplayedText = DisplayedText;
        TextClipboard.CopiedText = TextToCopy;
    }
}