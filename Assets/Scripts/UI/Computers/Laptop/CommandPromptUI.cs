using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_InputField outputField;

    [SerializeField]
    TextAsset cmdStartText;

    private string outputText;
    // public string OutputText { get; private set; }

    private const string CommandPromptStartText = ">>> ";
    private const string CommandPromptEndText = "\n";

    private void Awake()
    {
        outputText = string.Empty;
        outputField.text = string.Empty;
        inputField.text = CommandPromptStartText;

        SubmitCommand(cmdStartText.text, true);

        GameInput.OnCommandSubmitAction += () =>
        {
            string command = inputField.text.Remove(0, CommandPromptStartText.Length);
            SubmitCommand(command, false);
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

    private void SubmitCommand(string command, bool startingTXT)
    {
        inputField.text = CommandPromptStartText;
        inputField.ActivateInputField();
        if (!startingTXT)
        {
            outputText += CommandPromptStartText + command + CommandPromptEndText;
        }
        else
        {
            outputText += command;
        }
        outputField.text = outputText;
    }
}
