using System;
using UnityEngine;

public class CommandPromptManager : MonoBehaviour
{
    public Action<string> OnCommandResponse;

    [SerializeField]
    private PossibleCommandsSO possibleCommandsSO;

    private string currentCommand;

    private void OnDestroy()
    {
        possibleCommandsSO.ResetCommandGameEvents();
    }

    public void SubmitCommand(string command)
    {
        string commandLower = command.ToLower();

        if (commandLower.Length > 0)
        {
            string[] splitCommand = commandLower.Split(' ');
            string baseCommand = splitCommand[0];
            if (splitCommand.Length > 1)
            {
                string[] commandData = splitCommand[1..];
                ExecuteCommand(baseCommand, commandData);
            }
            else
            {
                ExecuteCommand(baseCommand, new string[0]);
            }
        }
    }

    private void ExecuteCommand(string command, string[] commandData)
    {
        if (command == string.Empty)
        {
            return;
        }

        if (possibleCommandsSO.PossibleCommandsDictionary.ContainsKey(command))
        {
            GameEventCommandDataSO gameEvent = possibleCommandsSO.PossibleCommandsDictionary[command];

            if (commandData.Length != gameEvent.RequiredArgumentsNumber)
            {
                RespondToCommand(false, "Invalid number of arguments");
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
            RespondToCommand(false, command + ": command not found.");
        }
    }

    private void RespondToCommand(bool wasSuccessful, string response)
    {
        string responseString = wasSuccessful ? "" : "Command failed. ";
        responseString += response;

        OnCommandResponse?.Invoke(responseString);
    }
}
