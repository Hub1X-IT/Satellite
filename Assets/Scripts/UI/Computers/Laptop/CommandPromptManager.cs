using System;
using UnityEngine;

public class CommandPromptManager : MonoBehaviour
{
    public Action<string[]> OnPrintCommandOutput;
    public Action OnCommandStatusChange;

    [SerializeField]
    private PossibleCommandsSO possibleCommandsSO;

    public bool IsCommandInProgress { get; private set; }

    private void Awake()
    {
        IsCommandInProgress = false;
    }

    private void OnDestroy()
    {
        possibleCommandsSO.ResetCommandGameEvents();
    }

    public void SubmitCommand(string inputCommand)
    {
        string[] command = inputCommand.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries); // splits only by spaces
        // string[] command = inputCommand.ToLower().Split((char[])null, StringSplitOptions.RemoveEmptyEntries); // splits by all whitespace

        if (command.Length > 0)
        {
            string baseCommand = command[0];
            string[] commandData = command[1..];

            ExecuteCommand(baseCommand, commandData);
        }
    }

    private void ExecuteCommand(string command, string[] commandData)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        if (possibleCommandsSO.PossibleCommandsDictionary.ContainsKey(command))
        {
            GameEventCommandDataSO gameEvent = possibleCommandsSO.PossibleCommandsDictionary[command];

            if (commandData.Length != gameEvent.RequiredArgumentsNumber)
            {
                RespondToCommand(new CommandResponseData
                {
                    ExecutionStatus = CommandExecutionStatus.Failure,
                    ResponseStringArray = new string[] { "Invalid number of arguments" }
                });
                return;
            }

            gameEvent.RaiseEvent(new CommandData()
            {
                CommandDataArray = commandData,
                Response = RespondToCommand,
            });
        }
        else
        {
            RespondToCommand(new CommandResponseData
            {
                ExecutionStatus = CommandExecutionStatus.Failure,
                ResponseStringArray = new string[] { command + ": command not found." }
            });
        }
    }

    private void RespondToCommand(CommandResponseData responseData)
    {
        Debug.Log("Command execution status: " + responseData.ExecutionStatus.ToString());

        OnPrintCommandOutput?.Invoke(responseData.ResponseStringArray);

        IsCommandInProgress = responseData.ExecutionStatus == CommandExecutionStatus.InProgress;
        
        OnCommandStatusChange?.Invoke();
    }
}
