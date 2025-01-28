using System;
using System.Collections.Generic;
using UnityEngine;

public static class CommandPromptManager
{
    [Serializable]
    public struct InitializationData
    {
        public PossibleCommandsSO possibleCommandsSO;
    }

    private static PossibleCommandsSO possibleCommandsSO;

    private static string currentCommand;

    private static Queue<string> previousCommandsQueue = new();

    public static void OnAwake(InitializationData data)
    {
        possibleCommandsSO = data.possibleCommandsSO;
    }

    public static void SubmitCommand(string command)
    {
        previousCommandsQueue.Enqueue(command);

        if (command.Length > 0)
        {
            string[] splitCommand = command.Split(' ');
            string baseCommand = splitCommand[0];
            if (splitCommand.Length > 1)
            {
                string[] commandData = splitCommand[1..];
                ExecuteCommand(baseCommand, commandData);
            }
        }
    }

    private static void ExecuteCommand(string command, string[] commandData)
    {
        if (possibleCommandsSO.PossibleCommandsDictionary.ContainsKey(command))
        {
            possibleCommandsSO.PossibleCommandsDictionary[command].RaiseEvent(new CommandData()
            {
                CommandDataArray = commandData,
                Response = RespondToCommand,
            });
        }
    }

    private static void RespondToCommand(bool wasSuccessful, string response)
    {
        Debug.Log(wasSuccessful);
        Debug.Log(response);
    }
}
