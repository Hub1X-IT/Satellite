/*
using TMPro;
using UnityEngine;

public class OldCommandPromptUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_InputField outputField;

    private string inputText;

    private string outputText;


    private void Start()
    {
        OldCommandPromptManager.CommandSubmitted += CommandPromptManager_CommandSubmitted;
        OldCommandPromptManager.CommandChanged += (command) => inputField.text = command;

        inputField.onDeselect.AddListener((_) => inputField.ActivateInputField());

        inputText = string.Empty;
        outputText = string.Empty;
        outputField.text = string.Empty;
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
            outputField.text = outputText;
        }
    }

    private void AddOutputText(string text)
    {
        outputText += text;
        outputField.text = outputText;
    }
}
*/