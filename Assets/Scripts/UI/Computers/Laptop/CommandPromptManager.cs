using System;

public static class CommandPromptManager
{
    [Serializable]
    public struct InitializationData
    {
        public PossibleCommandsSO possibleCommandsSO;
    }

    public static Action<string> CommandResponse;

    private static PossibleCommandsSO possibleCommandsSO;

    private static string currentCommand;

    public static void OnAwake(InitializationData data)
    {
        possibleCommandsSO = data.possibleCommandsSO;
    }

    public static void OnSceneExit()
    {
        possibleCommandsSO.ResetCommandGameEvents();
    }

    public static void SubmitCommand(string command)
    {
        if (command.Length > 0)
        {
            string[] splitCommand = command.Split(' ');
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

    private static void ExecuteCommand(string command, string[] commandData)
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

    private static void RespondToCommand(bool wasSuccessful, string response)
    {
        string responseString = wasSuccessful ? "" : "Command failed. ";
        responseString += response;

        CommandResponse?.Invoke(responseString);
    }
}
