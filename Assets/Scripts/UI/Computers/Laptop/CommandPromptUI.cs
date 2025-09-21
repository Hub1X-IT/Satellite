using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputTextField;

    [SerializeField]
    private TMP_Text outputTextFieldPrefab;

    [SerializeField]
    private RectTransform outputField;

    [SerializeField]
    private TextAsset cmdStartText;

    [SerializeField]
    private AudioSource cmdAudioSource;

    /*
    [SerializeField]
    private ScrollRect outputFieldScrollRect;
    */

    private string outputText;

    private List<string> previousCommandsList;

    private string currentCommandCache;
    private int currentCommandIndex;
    private bool isOnCurrentCommand;
    private bool canGetPreviousCommand;

    private bool shouldFocusOnInputFieldNextFrame;

    private void Awake()
    {
        outputText = string.Empty;
        outputTextFieldPrefab.text = string.Empty;

        previousCommandsList = new();

        GameInput.OnCommandSubmitAction += () =>
        {
            string command = GetCurrentCommand();
            SubmitCommand(command);
            CommandPromptManager.SubmitCommand(command);
        };

        GameInput.OnPreviousCommandAction += TrySetPreviousCommand;
        GameInput.OnNextCommandAction += TrySetNextCommand;

        CommandPromptManager.CommandResponse += (responseString) =>
        {
            SubmitResponse(responseString);
        };

        inputTextField.onSelect.AddListener((_) =>
        {
            inputTextField.caretPosition = inputTextField.text.Length;
        });


        inputTextField.onValueChanged.AddListener((_) =>
        {
            cmdAudioSource.Play();
        });

        //SetStartupText(cmdStartText.text + "\n");

        currentCommandCache = "";
        currentCommandIndex = -1;
        isOnCurrentCommand = true;
        canGetPreviousCommand = false;

        shouldFocusOnInputFieldNextFrame = false;
    }

    private void LateUpdate()
    {
        if (shouldFocusOnInputFieldNextFrame)
        {
            FocusOnInputField();
        }
    }

    private void SubmitCommand(string command)
    {
        previousCommandsList.Add(command);
        isOnCurrentCommand = true;
        canGetPreviousCommand = true;

        inputTextField.text = "";
        FocusOnInputField();

        TMP_Text outputTextField = Instantiate(outputTextFieldPrefab.gameObject, outputField).GetComponent<TMP_Text>();
        outputTextField.text = ">>> " + command;
    }

    private void SubmitResponse(string responseText)
    {
        TMP_Text outputTextField = Instantiate(outputTextFieldPrefab.gameObject, outputField).GetComponent<TMP_Text>();
        outputTextField.text = responseText;
    }

    private string GetCurrentCommand()
    {
        string command = inputTextField.text;
        return command;
    }

    private void SetInputFieldText(string text)
    {
        inputTextField.text = text;
    }

    private void SetStartupText(string text)
    {
        outputText = text;
        outputTextFieldPrefab.text = outputText;
    }

    public void FocusOnInputField()
    {
        inputTextField.ActivateInputField();
        inputTextField.caretPosition = inputTextField.text.Length;
    }

    private void TrySetPreviousCommand()
    {
        if (canGetPreviousCommand)
        {
            if (isOnCurrentCommand)
            {
                if (TryGetLastCommand(out string previousCommand, out currentCommandIndex))
                {
                    currentCommandCache = GetCurrentCommand();
                    SetInputFieldText(previousCommand);
                    isOnCurrentCommand = false;
                }
                else
                {
                    canGetPreviousCommand = false;
                }
            }
            else
            {
                if (TryGetCommandWithIndex(currentCommandIndex - 1, out string previousCommand))
                {
                    currentCommandIndex--;
                    SetInputFieldText(previousCommand);
                }
                else
                {
                    canGetPreviousCommand = false;
                }
            }
        }
        else
        {
            Debug.Log("no previous commands.");
        }

        shouldFocusOnInputFieldNextFrame = true;
    }

    private void TrySetNextCommand()
    {
        if (!isOnCurrentCommand)
        {
            if (TryGetCommandWithIndex(currentCommandIndex + 1, out string nextCommand))
            {
                currentCommandIndex++;
                SetInputFieldText(nextCommand);
            }
            else
            {
                SetInputFieldText(currentCommandCache);
                currentCommandCache = string.Empty;
                isOnCurrentCommand = true;
            }
            canGetPreviousCommand = true;
        }

        shouldFocusOnInputFieldNextFrame = true;
    }

    private bool TryGetCommandWithIndex(int commandIndex, out string command)
    {
        if (previousCommandsList != null && commandIndex >= 0 && commandIndex < previousCommandsList.Count)
        {
            command = previousCommandsList[commandIndex];
            return true;
        }
        command = string.Empty;
        return false;
    }

    private bool TryGetLastCommand(out string command, out int commandIndex)
    {
        if (previousCommandsList != null && previousCommandsList.Count > 0)
        {
            commandIndex = previousCommandsList.Count - 1;
            command = previousCommandsList[commandIndex];
            return true;
        }

        command = string.Empty;
        commandIndex = -1;
        return false;
    }
}
