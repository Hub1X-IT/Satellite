using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandPromptUI : MonoBehaviour
{
    [SerializeField]
    private CommandPromptManager commandPromptManager;

    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TMP_Text placeholderTextField;

    private const string DefaultPlaceholderText = "Enter command...";
    private const string CommandInProgressPlaceholderText = "Command execution in progress...";

    [SerializeField]
    private TMP_Text outputTextFieldPrefab;
    [SerializeField]
    private TMP_Text defaultOutputTextField;

    [SerializeField]
    private RectTransform outputField;

    [SerializeField]
    private TextAsset cmdStartText;

    [SerializeField]
    private AudioSource cmdAudioSource;

    private Button outputFieldButton;

    private List<string> previousCommandsList;

    private string currentCommandCache;
    private int currentCommandIndex;
    private bool isOnCurrentCommand;
    private bool canGetPreviousCommand;

    private bool isCommandPromptEnabled;
    private bool isInputFieldInteractable;

    private bool shouldToggleFocus;

    private void Awake()
    {
        outputTextFieldPrefab.text = "";

        previousCommandsList = new();

        outputFieldButton = GetComponent<Button>();

        currentCommandCache = "";
        currentCommandIndex = -1;
        isOnCurrentCommand = true;
        canGetPreviousCommand = false;

        isCommandPromptEnabled = false;
        isInputFieldInteractable = true;
    }

    private void Start()
    {
        GameInput.Instance.OnCommandSubmitAction += () =>
        {
            string command = GetCurrentCommand();
            SubmitCommand(command);
            commandPromptManager.SubmitCommand(command);
        };

        GameInput.Instance.OnPreviousCommandAction += TrySetPreviousCommand;
        GameInput.Instance.OnNextCommandAction += TrySetNextCommand;

        commandPromptManager.OnPrintCommandOutput += SubmitOutput;
        commandPromptManager.OnCommandStatusChange += OnCommandStatusChange;

        inputField.onSelect.AddListener((_) => MoveCaretToEnd());

        inputField.onValueChanged.AddListener((_) => PlayKeyboardSound());

        outputFieldButton.onClick.AddListener(ToggleInputFieldFocus);

        defaultOutputTextField.text = cmdStartText.text;
        placeholderTextField.text = DefaultPlaceholderText;
    }

    // Input field focus handling
    private void LateUpdate()
    {
        if (shouldToggleFocus)
        {
            if (isCommandPromptEnabled && isInputFieldInteractable)
            {
                inputField.ActivateInputField();
                MoveCaretToEnd();
            }
            else
            {
                inputField.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
            }
            shouldToggleFocus = false;
        }
    }

    private void ToggleInputFieldFocus()
    {
        shouldToggleFocus = true;
    }

    private void MoveCaretToEnd()
    {
        inputField.MoveTextEnd(false);
    }

    // May not be the best solution
    public void SetCommandPromptEnabled(bool enabled)
    {
        inputField.enabled = enabled;
        isCommandPromptEnabled = enabled;
        ToggleInputFieldFocus();
    }

    private void OnCommandStatusChange()
    {
        isInputFieldInteractable = !commandPromptManager.IsCommandInProgress;
        inputField.interactable = isInputFieldInteractable;
        placeholderTextField.text = isInputFieldInteractable ? DefaultPlaceholderText : CommandInProgressPlaceholderText;
        ToggleInputFieldFocus();
    }

    // Current commands
    private void SubmitCommand(string command)
    {
        previousCommandsList.Add(command);
        isOnCurrentCommand = true;
        canGetPreviousCommand = true;

        inputField.text = "";
        ToggleInputFieldFocus();

        TMP_Text outputTextField = Instantiate(outputTextFieldPrefab.gameObject, outputField).GetComponent<TMP_Text>();
        outputTextField.text = ">>> " + command;
    }
    private void SubmitOutput(string[] multilineResponse)
    {
        foreach (var line in multilineResponse)
        {
            TMP_Text outputTextField = Instantiate(outputTextFieldPrefab.gameObject, outputField).GetComponent<TMP_Text>();
            outputTextField.text = line;
        }
    }
    private string GetCurrentCommand()
    {
        return inputField.text;
    }
    private void SetInputFieldText(string text)
    {
        inputField.text = text;
    }

    // Previous commands
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

        ToggleInputFieldFocus();
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
                currentCommandCache = "";
                isOnCurrentCommand = true;
            }
            canGetPreviousCommand = true;
        }

        ToggleInputFieldFocus();
    }

    private bool TryGetCommandWithIndex(int commandIndex, out string command)
    {
        if (previousCommandsList != null && commandIndex >= 0 && commandIndex < previousCommandsList.Count)
        {
            command = previousCommandsList[commandIndex];
            return true;
        }
        command = "";
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

        command = "";
        commandIndex = -1;
        return false;
    }

    private void PlayKeyboardSound()
    {
        cmdAudioSource.Play();
    }
}
