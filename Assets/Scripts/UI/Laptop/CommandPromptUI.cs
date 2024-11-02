using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_Text outputTextField;


    private string inputText;

    private string outputText;


    private void Start()
    {
        CommandPromptManager.CommandSubmitted += CommandPromptManager_CommandSubmitted;
        CommandPromptManager.CommandChanged += (command) => inputField.text = command;

        inputField.onDeselect.AddListener((_) => inputField.ActivateInputField());
    }

    private void CommandPromptManager_CommandSubmitted()
    {
        inputText = inputField.text;
        ChangeOutputText();
        inputField.text = string.Empty;
        // inputField.ActivateInputField();
    }

    private void ChangeOutputText()
    {
        if (inputText.Length > 0)
        {
            outputText += "\n>>> " + inputText;
            outputTextField.text = outputText;
        }
    }

    private void AddOutputText(string text)
    {
        outputText += text;
        outputTextField.text = outputText;
    }
}
