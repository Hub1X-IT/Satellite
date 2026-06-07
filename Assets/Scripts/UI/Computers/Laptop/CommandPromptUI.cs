using System.Collections.Generic;
using DG.Tweening;
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

    [SerializeField]
    private TMP_Text cmdArrows;

    private Button outputFieldButton;

    private List<string> previousCommandsList;

    private string currentCommandCache;
    private int currentCommandIndex;
    private bool isOnCurrentCommand;
    private bool canGetPreviousCommand;

    private bool isCommandPromptEnabled;
    private bool isInputFieldInteractable;

    private bool shouldToggleFocus;

    private Sequence inputPromptSequence;

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

    private void OnDestroy()
    {
        inputPromptSequence?.Kill();
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

        if (commandPromptManager.IsCommandInProgress)
        {
            StartInputPromptAnimation();
        }
        else
        {
            StopInputPromptAnimation();
        }
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

    private void StartInputPromptAnimation()
    {
        cmdArrows.SetText("...");
        Color from = cmdArrows.color;
        Color to = new(from.r, from.g, from.b, 0.125f);
        inputPromptSequence?.Kill();

        // Virtual tweens aren't officially supported in sequences, so if any issues will arise with tweaning, this might be the cause.
        // None noticed so far.
        inputPromptSequence = DOTween.Sequence()
            .Insert(0.00f, cmdArrows.DOCharColor(0, from, to, 1))
            .Insert(0.25f, cmdArrows.DOCharColor(1, from, to, 1))
            .Insert(0.50f, cmdArrows.DOCharColor(2, from, to, 1))

            .Insert(1.75f, cmdArrows.DOCharColor(0, to, from, 1))
            .Insert(2.00f, cmdArrows.DOCharColor(1, to, from, 1))
            .Insert(2.25f, cmdArrows.DOCharColor(2, to, from, 1))

            // .AppendInterval(0.25f)
            .SetLoops(-1);
    }

    private void StopInputPromptAnimation()
    {
        inputPromptSequence?.Kill();
        cmdArrows.SetText(">>>");
    }
}
