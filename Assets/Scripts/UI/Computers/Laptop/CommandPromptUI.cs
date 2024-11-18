using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputTextField;

    [SerializeField]
    TMP_InputField outputTextField;

    [SerializeField]
    TextAsset cmdStartText;

    private string outputText;

    private const string CommandPromptStartText = ">>> ";
    private const string CommandPromptEndText = "\n";

    private void Awake()
    {
        outputText = string.Empty;
        outputTextField.text = string.Empty;
        inputTextField.text = CommandPromptStartText;

        GameInput.OnCommandSubmitAction += () =>
        {
            string command = inputTextField.text.Remove(0, CommandPromptStartText.Length);
            SubmitCommand(command);
            CommandPromptManager.SubmitCommand(command);
        };

        inputTextField.onValueChanged.AddListener((text) =>
        {
            if (text.Length < CommandPromptStartText.Length)
            {
                inputTextField.text = CommandPromptStartText;
                inputTextField.caretPosition = inputTextField.text.Length;
            }
        });

        SetStartupText(cmdStartText.text + "\n");
    }

    private void SubmitCommand(string command)
    {
        inputTextField.text = CommandPromptStartText;
        inputTextField.ActivateInputField();
        outputText += CommandPromptStartText + command + CommandPromptEndText;
        outputTextField.text = outputText;
    }

    private void SetStartupText(string text)
    {
        outputText = text;
        outputTextField.text = outputText;
    }
}
