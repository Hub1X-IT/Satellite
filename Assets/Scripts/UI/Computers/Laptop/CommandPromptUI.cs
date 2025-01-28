using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputTextField;

    [SerializeField]
    private TMP_Text outputTextField;

    [SerializeField]
    private RectTransform outputField;

    [SerializeField]
    private TextAsset cmdStartText;

    /*
    [SerializeField]
    private ScrollRect outputFieldScrollRect;
    */

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
            /*
            for (int i = 0; i < CommandPromptStartText.Length; i++)
            {
                if (text[i] != CommandPromptStartText[i])
                {
                    
                }
            }
            */
        });

        //SetStartupText(cmdStartText.text + "\n");
    }

    private void SubmitCommand(string command)
    {
        inputTextField.text = CommandPromptStartText;
        inputTextField.ActivateInputField();

        //outputText += CommandPromptStartText + command + CommandPromptEndText;
        outputTextField.text = CommandPromptStartText + command;
        //outputTextField.text = outputText;

        // outputFieldScrollRect.verticalNormalizedPosition = 0;

        // outputTextField.caretPosition = outputTextField.text.Length;

        Instantiate(outputTextField, outputField);
    }

    private void SetStartupText(string text)
    {
        outputText = text;
        outputTextField.text = outputText;
    }
}
