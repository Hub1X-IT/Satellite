using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_InputField outputField;

    private string outputText;
    // public string OutputText { get; private set; }

    private const string CommandPromptStartText = ">>> ";
    private const string CommandPromptEndText = "\n";

    private void Awake()
    {
        outputText = string.Empty;
        outputField.text = string.Empty;
        inputField.text = CommandPromptStartText;

        GameInput.OnCommandSubmitAction += () =>
        {
            string command = inputField.text.Remove(0, CommandPromptStartText.Length);
            SubmitCommand(command);
            CommandPromptManager.SubmitCommand(command);
        };

        inputField.onValueChanged.AddListener((text) =>
        {
            if (text.Length < CommandPromptStartText.Length)
            {
                inputField.text = CommandPromptStartText;
                inputField.caretPosition = inputField.text.Length;
            }
        });
    }

    private void SubmitCommand(string command)
    {
        inputField.text = CommandPromptStartText;
        inputField.ActivateInputField();
        outputText += CommandPromptStartText + command + CommandPromptEndText;
        outputField.text = outputText;
    }
}
